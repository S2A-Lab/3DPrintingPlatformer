using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public int filament = 0;
    public int metal = 0;
    public Queue<BlockData> blocks = new Queue<BlockData>();
    public UiManager uimanager;

    public void AddFilament(int amount)
    {
        filament += amount;
        uimanager.UpdateInventory(filament, metal);
    }

    public void AddMetal(int amount)
    {
        metal += amount;
        uimanager.UpdateInventory(filament, metal);
    }


    public bool BuyPrint(BlockData block)
    {
        if (block.matType == MatType.FILAMENT && filament >= block.materialCost)
        {
            AddFilament(-block.materialCost); 
            return true;
        }
        if (block.matType == MatType.METAL && metal >= block.materialCost)
        {
            AddMetal(-block.materialCost); 
            return true;
        }
        return false;
    }

    public void MergeBlocks(List<BlockData> incomingBlocks)
    {
        foreach (BlockData block in incomingBlocks)
        {
            blocks.Enqueue(block);
        }

    }

    public void CycleBlocks()
    {
        if (blocks.Count == 0) return;

        BlockData temp = blocks.Dequeue();
        blocks.Enqueue(temp);
    }

    public BlockData GetNextBlock()
    {
        if (blocks.Count == 0) return null;
        return blocks.Peek();
    }

    public void RemoveBlock()
    {
        if (blocks.Count == 0) return;
        blocks.Dequeue();
        
    }

    public bool HasBlocks()
    {
        return blocks.Count > 0;
    }
}
