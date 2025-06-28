using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class BuilderMode : MonoBehaviour
{
    public PlayerInventory inventory;
    public bool isBuilding = false;

    [Header("References")]
    public Grid grid;

    [Header("Settings")]
    public float placementCooldown = 0.05f;

    private BlockData currentBlock;
    private HashSet<Vector3> blockPositions = new HashSet<Vector3>();
    private Camera mainCamera;
    private float lastPlacementTime = 0f;
    private Vector3 lastSnappedPosition = Vector3.positiveInfinity;
    private GameObject currentGhostBlock;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (!isBuilding || currentBlock == null || grid == null) return;

        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 0f));
        mouseWorldPos.z = 0f;

        Vector3Int gridPos = grid.WorldToCell(mouseWorldPos);
        Vector3 snappedWorldPos = grid.CellToWorld(gridPos);
        snappedWorldPos += grid.cellSize * 0.5f;

        // Only update ghost block position if it changed
        if (snappedWorldPos != lastSnappedPosition)
        {
            if (currentGhostBlock == null)
            {
                currentGhostBlock = Instantiate(currentBlock.blockPrefab);
                MakeGhostTransparent(currentGhostBlock);
            }
            currentGhostBlock.transform.position = snappedWorldPos;
            lastSnappedPosition = snappedWorldPos;
        }

        // Placement logic
        if (Mouse.current.leftButton.isPressed && inventory.HasBlocks())
        {
            if (!blockPositions.Contains(snappedWorldPos) &&
                Time.time >= lastPlacementTime + placementCooldown)
            {
                PlaceBlock(snappedWorldPos);
                lastPlacementTime = Time.time;
            }
        }
    }

    public void ToggleIsBuilding(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        isBuilding = !isBuilding;

        if (isBuilding)
        {
            if (inventory.HasBlocks())
            {
                currentBlock = inventory.GetNextBlock();
                Debug.Log("Building mode ON");
            }
            else
            {
                Debug.Log("No blocks in inventory.");
                isBuilding = false;
            }
        }
        else
        {
            DestroyGhostBlock();
            lastSnappedPosition = Vector3.positiveInfinity;
            Debug.Log("Building mode OFF");
        }
    }

    private void PlaceBlock(Vector3 position)
    {
        Instantiate(currentBlock.blockPrefab, position, Quaternion.identity);
        blockPositions.Add(position);
        inventory.RemoveBlock();
        
        if (inventory.HasBlocks())
        {
            currentBlock = inventory.GetNextBlock();
        }
        else
        {
            Debug.Log("Out of blocks, building mode OFF.");
            isBuilding = false;
            DestroyGhostBlock();
        }
    }

    private void MakeGhostTransparent(GameObject ghost)
    {
        foreach (SpriteRenderer sr in ghost.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = sr.color;
            color.a = 0.5f;
            sr.color = color;
        }
    }

    private void DestroyGhostBlock()
    {
        if (currentGhostBlock != null)
        {
            Destroy(currentGhostBlock);
            currentGhostBlock = null;
        }
    }
}
