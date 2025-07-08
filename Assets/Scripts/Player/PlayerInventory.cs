using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerInventory : MonoBehaviour
{
    public int filament = 0;
    public int metal = 0;
    public int resin = 0;
    public int rubber = 0;

    public int filamentBlocks = 0;
    public int metalBlocks = 0;
    public int resinBlocks = 0;
    public int rubberBlocks = 0;

    public Dictionary<BlockData, int> blockCounts = new Dictionary<BlockData, int>();
    public UiManager uimanager;

    public void AddMaterial(MatType type, int amount)
    {
        switch (type)
        {
            case MatType.FILAMENT:
                filament += amount;
                break;
            case MatType.METAL:
                metal += amount;
                break;
            case MatType.RESIN:
                resin += amount;
                break;
            case MatType.RUBBER:
                rubber += amount;
                break;
        }

        uimanager.UpdateInventory(filament, metal, resin, rubber);
    }

    public bool BuyPrint(BlockData block)
    {
        switch (block.matType)
        {
            case MatType.FILAMENT when filament >= block.materialCost:
                AddMaterial(MatType.FILAMENT, -block.materialCost);
                return true;
            case MatType.METAL when metal >= block.materialCost:
                AddMaterial(MatType.METAL, -block.materialCost);
                return true;
            case MatType.RESIN when resin >= block.materialCost:
                AddMaterial(MatType.RESIN, -block.materialCost);
                return true;
            case MatType.RUBBER when rubber >= block.materialCost:
                AddMaterial(MatType.RUBBER, -block.materialCost);
                return true;
        }

        return false;
    }

    public void AddBlock(BlockData block)
    {
        if (blockCounts.ContainsKey(block))
            blockCounts[block]++;
        else
            blockCounts[block] = 1;

        CountAddBlock(block);
    }

    public void RemoveBlock(BlockData block)
    {
        if (!blockCounts.ContainsKey(block)) return;

        blockCounts[block]--;
        if (blockCounts[block] <= 0)
            blockCounts.Remove(block);
        CountRemoveBlock(block);
    }

    public int GetBlockCount(BlockData block)
    {
        return blockCounts.TryGetValue(block, out int count) ? count : 0;
    }

    public bool HasBlockType(BlockData block)
    {
        return blockCounts.ContainsKey(block);
    }

    public bool HasBlocks()
    {
        return blockCounts.Count > 0;
    }

    public BlockData GetBlockTypeAtIndex(int index)
    {
        if (index < 0 || index >= blockCounts.Count) return null;
        return blockCounts.Keys.ElementAt(index);
    }

    public int GetTotalBlockTypes()
    {
        return blockCounts.Count;
    }

    private void CountAddBlock(BlockData block)
    {
        switch (block.matType)
        {
            case MatType.FILAMENT:
                filamentBlocks++;
                break;
            case MatType.METAL:
                metalBlocks++;
                break;
            case MatType.RESIN:
                resinBlocks++;
                break;
            case MatType.RUBBER:
                rubberBlocks++;
                break;
        }
         uimanager.UpdateMaterialsInventory(filamentBlocks, metalBlocks, rubberBlocks, resinBlocks); 

    }

    private void CountRemoveBlock(BlockData block)
    {
        switch (block.matType)
        {
            case MatType.FILAMENT:
                filamentBlocks--;
                break;
            case MatType.METAL:
                metalBlocks--;
                break;
            case MatType.RESIN:
                resinBlocks--;
                break;
            case MatType.RUBBER:
                rubberBlocks--;
                break;
        }

        uimanager.UpdateMaterialsInventory(filamentBlocks, metalBlocks, rubberBlocks, resinBlocks); 
    }


}
