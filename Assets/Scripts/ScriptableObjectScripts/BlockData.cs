using UnityEngine;

[CreateAssetMenu(fileName = "BlockData", menuName = "Scriptable Objects/BlockData")]
public class BlockData : ScriptableObject
{
    [Header("Block Properties")]
    public string blockName;
    public Sprite icon;
    public GameObject blockPrefab;

    [Header("Printing Properties")]
    public int resolution;
    public MatType matType;
    public int printTime;
    public int materialCost;
}
