using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Linq;

public class BuilderMode : MonoBehaviour
{
    public PlayerInventory inventory;
    public bool isBuilding = false;

    [Header("References")]
    public Grid grid;

    [Header("Settings")]
    private float placementCooldown = 0.05f;

    private BlockData currentBlock;
    private HashSet<Vector3> blockPositions = new HashSet<Vector3>();
    private Camera mainCamera;
    private float lastPlacementTime = 0f;
    private Vector3 lastSnappedPosition = Vector3.positiveInfinity;
    private GameObject currentGhostBlock;
    private int currentBlockIndex = 0;

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

        if (snappedWorldPos != lastSnappedPosition)
        {
            UpdateGhostBlock(snappedWorldPos);
            lastSnappedPosition = snappedWorldPos;
        }

        Vector2 scrollDelta = Mouse.current.scroll.ReadValue();
        if (scrollDelta.y != 0)
        {
            SwitchBlock(scrollDelta.y > 0 ? 1 : -1);
        }

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
        if (!context.performed) return;

        isBuilding = !isBuilding;

        if (isBuilding)
        {
            if (inventory.HasBlocks())
            {
                currentBlockIndex = 0;
                currentBlock = inventory.GetBlockTypeAtIndex(currentBlockIndex);
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

    private void SwitchBlock(int direction)
    {
        if (!inventory.HasBlocks()) return;

        int totalBlocks = inventory.GetTotalBlockTypes();
        currentBlockIndex = (currentBlockIndex + direction) % totalBlocks;
        if (currentBlockIndex < 0)
            currentBlockIndex = totalBlocks - 1;

        currentBlock = inventory.GetBlockTypeAtIndex(currentBlockIndex);

        if (currentGhostBlock != null)
        {
            DestroyGhostBlock();
            UpdateGhostBlock(lastSnappedPosition);
        }

        Debug.Log($"Switched to block: {currentBlock.name}");
    }

    private void UpdateGhostBlock(Vector3 position)
    {
        if (currentGhostBlock == null)
        {
            currentGhostBlock = Instantiate(currentBlock.blockPrefab);
            MakeGhostTransparent(currentGhostBlock);
            RemoveColliders(currentGhostBlock);
        }
        currentGhostBlock.transform.position = position;
    }

    private void PlaceBlock(Vector3 position)
    {
        Instantiate(currentBlock.blockPrefab, position, Quaternion.identity);
        blockPositions.Add(position);
        inventory.RemoveBlock(currentBlock);

        if (inventory.HasBlocks())
        {
            if (!inventory.HasBlockType(currentBlock))
            {
                currentBlockIndex = 0;
                currentBlock = inventory.GetBlockTypeAtIndex(currentBlockIndex);
                DestroyGhostBlock();
            }
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
            sr.material = new Material(Shader.Find("Sprites/Default"));
            Color color = sr.color;
            color.a = 0.5f;
            sr.color = color;
            sr.sortingOrder += 1000;
        }
    }

    private void RemoveColliders(GameObject ghost)
    {
        foreach (Collider2D col in ghost.GetComponentsInChildren<Collider2D>())
        {
            Destroy(col);
        }

        foreach (Collider col in ghost.GetComponentsInChildren<Collider>())
        {
            Destroy(col);
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
