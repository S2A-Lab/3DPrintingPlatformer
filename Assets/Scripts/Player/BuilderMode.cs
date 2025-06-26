using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class BuilderMode : MonoBehaviour
{
    public bool isBuilding = false;

    [Header("Prefabs")]
    public GameObject ghostBlockPrefab;
    public GameObject realBlockPrefab;

    [Header("References")]
    public Grid grid;

    [Header("Settings")]
    public float printTime = 2.0f;
    public float placementCooldown = 0.05f; // Reduced cooldown for smoother placement

    [Header("Physics")]
    public PhysicsMaterial2D frictionlessMaterial;

    private GameObject currentGhostBlock;
    private HashSet<Vector3> ghostBlockPositions = new HashSet<Vector3>(); // Changed to HashSet for faster lookups
    private Camera mainCamera;
    private float lastPlacementTime = 0f;
    private Vector3 lastSnappedPosition = Vector3.positiveInfinity; // Track last position to avoid redundant updates

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (!isBuilding || ghostBlockPrefab == null || grid == null) return;

        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 0f));
        mouseWorldPos.z = 0f;

        Vector3Int gridPos = grid.WorldToCell(mouseWorldPos);
        Vector3 snappedWorldPos = grid.CellToWorld(gridPos);
        snappedWorldPos += grid.cellSize * 0.5f; // Center the block in the grid cell

        // Only update ghost block position if it changed
        if (snappedWorldPos != lastSnappedPosition)
        {
            if (currentGhostBlock == null)
            {
                currentGhostBlock = Instantiate(ghostBlockPrefab);
            }
            currentGhostBlock.transform.position = snappedWorldPos;
            lastSnappedPosition = snappedWorldPos;
        }

        // Smoother placement logic
        if (Mouse.current.leftButton.isPressed && GetComponent<PlayerInventory>().filament > 0)
        {
            if (!ghostBlockPositions.Contains(snappedWorldPos) && 
                Time.time >= lastPlacementTime + placementCooldown)
            {
                PlaceGhostBlock(snappedWorldPos);
                lastPlacementTime = Time.time;
            }
        }
    }

    public void ToggleIsBuilding(InputAction.CallbackContext context)
    {
        if (context.started)
            isBuilding = !isBuilding;

        if (!isBuilding)
        {
            if (currentGhostBlock != null)
            {
                Destroy(currentGhostBlock);
                currentGhostBlock = null;
            }
            lastSnappedPosition = Vector3.positiveInfinity; // Reset position tracking
        }
    }

    private void PlaceGhostBlock(Vector3 position)
    {
        GameObject ghostBlock = Instantiate(ghostBlockPrefab, position, Quaternion.identity);
        
        // Ensure perfect alignment
        ghostBlock.transform.position = position;
        
        ghostBlockPositions.Add(position);
        
        // Start individual print coroutine for this block
        StartCoroutine(PrintIndividualBlock(position, ghostBlock));
    }

    private IEnumerator PrintIndividualBlock(Vector3 position, GameObject ghostBlock)
    {
        // Wait for the print time
        yield return new WaitForSeconds(printTime);

        // Check if the ghost block still exists (in case building was cancelled)
        if (ghostBlock != null)
        {
            // Create the real block with exact positioning
            GameObject realBlock = Instantiate(realBlockPrefab, position, Quaternion.identity);
            
            // Ensure perfect alignment
            realBlock.transform.position = position;

            // Remove Rigidbody2D if the prefab has one (no physics needed)
            Rigidbody2D blockRb = realBlock.GetComponent<Rigidbody2D>();
            if (blockRb != null) 
                Destroy(blockRb);

            // Configure the collider as a simple trigger or static collider
            BoxCollider2D col = realBlock.GetComponent<BoxCollider2D>();
            if (col != null)
            {
                col.usedByComposite = false; // Not using composite collider
                col.sharedMaterial = frictionlessMaterial;
            }

            // Destroy the ghost block
            Destroy(ghostBlock);

            // Remove from tracking set
            ghostBlockPositions.Remove(position);
        }
    }



    public void ClearAllGhostBlocks()
    {
        // Stop all running coroutines to prevent blocks from printing
        StopAllCoroutines();

        // Destroy all ghost blocks
        GameObject[] ghosts = GameObject.FindGameObjectsWithTag("GhostBlock");
        foreach (GameObject ghost in ghosts)
        {
            Destroy(ghost);
        }

        ghostBlockPositions.Clear();

        if (currentGhostBlock != null)
        {
            Destroy(currentGhostBlock);
            currentGhostBlock = null;
        }
        
        lastSnappedPosition = Vector3.positiveInfinity; // Reset position tracking
    }

    // Optional: Method to cancel all pending prints
    public void CancelAllPendingPrints()
    {
        StopAllCoroutines();
        ghostBlockPositions.Clear();
    }
}