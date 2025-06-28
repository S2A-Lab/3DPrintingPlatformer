using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrinterUIController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform queueParentContainer; // Parent object holding the queue images
    [SerializeField] private GameObject blockIconPrefab;     // Prefab representing a block in the queue

    [SerializeField] private Transform TrayParentContainer; 


    [SerializeField] private TMP_Text printTimeTxt;
    [SerializeField] private TMP_Text materialCostTxt;

    public PlayerInventory inventory; 

    public Printer printer;
    private BlockData currentBlock;

  

    public void SetCurrentBlock(BlockData block)
    {
        currentBlock = block;
        UpdateSelectedBlockUI();
    }

    public void PrintCurrentBlock()
    {
        if (currentBlock == null || printer == null)
            return;

        printer.AddToPrintQueue(currentBlock);
       

    }
    public void CollectFinishedBlocks()
    {
        inventory.MergeBlocks(printer.GetFinishedPrints());
        printer.ClearFinishedPrints();

    }

    public void PushUIQueue(BlockData block)
    {
        GameObject icon = Instantiate(blockIconPrefab, queueParentContainer);

        // Example: If your prefab has an Image component to show an icon
        Image iconImage = icon.GetComponent<Image>();
        if (iconImage != null && block.icon != null)
            iconImage.sprite = block.icon;

        Debug.Log($"Added {block.blockName} to UI queue.");
    }

    public void PopUIQueue()
    {
        if (queueParentContainer.childCount == 0)
            return;

        Transform firstChild = queueParentContainer.GetChild(0);
        Destroy(firstChild.gameObject);

        Debug.Log("Removed first item from UI queue.");
    }

    public void PushUITray(BlockData block)
    {
        GameObject icon = Instantiate(blockIconPrefab, TrayParentContainer);
        
        // Example: If your prefab has an Image component to show an icon
        Image iconImage = icon.GetComponent<Image>();
        if (iconImage != null && block.icon != null)
            iconImage.sprite = block.icon;

        Debug.Log($"Added {block.blockName} to UI queue.");
    }

  public void ClearUITray()
{
    foreach (Transform child in TrayParentContainer)
    {
        Destroy(child.gameObject);
    }

    Debug.Log("Cleared printer tray UI.");
}


    private void UpdateSelectedBlockUI()
    {
        printTimeTxt.text = "" + currentBlock.printTime;
        materialCostTxt.text = "" + currentBlock.materialCost;

    }

 
}
