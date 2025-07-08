using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrinterUIController : MonoBehaviour
{



    [Header("UI References")]

    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private Transform queueParentContainer; // Parent object holding the queue images
    [SerializeField] private GameObject blockIconPrefab;     // Prefab representing a block in the queue
     [SerializeField] private BlockData plasticBlockData; 
  


    [SerializeField] private TMP_Text printTimeTxt;
    [SerializeField] private TMP_Text materialCostTxt;
    [SerializeField] private TMP_Text itemNametxt;

    [SerializeField] private Image blueprint_image;

    [SerializeField] private Image current_print_image;

    public PlayerInventory inventory;

    public Printer printer;
    private BlockData currentBlock;

    [SerializeField] private Button plasticBlock_btn;
    [SerializeField] private Button resinBlock_btn;
    [SerializeField] private Button metalBlock_btn;
    [SerializeField] private Button rubberBlock_btn;

    void Start()
    {
        SetCurrentBlock(plasticBlockData); 
    }


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




    private void UpdateSelectedBlockUI()
    {
        printTimeTxt.text = "" + currentBlock.printTime;
        materialCostTxt.text = "" + currentBlock.materialCost;
        itemNametxt.text = "" + currentBlock.blockName;
        blueprint_image.sprite = currentBlock.icon;

    }

    public void RunCurrentPrint(float value, BlockData block)
    {
        progressBar.Run(value);
        current_print_image.sprite = block.icon;
    }
    public void ResetCurrentPrint()
    {
        current_print_image.sprite = null;
    }

    public void UnlockButton(MatType type)
    {
           switch (type)
            {
                case MatType.FILAMENT:
                plasticBlock_btn.interactable = true; 
                    break;
                case MatType.METAL:
                    metalBlock_btn.interactable = true; 
                    break;
                case MatType.RESIN:
                resinBlock_btn.interactable = true; 
                    break;
                case MatType.RUBBER:
                rubberBlock_btn.interactable = true;    
                    break;
        }
    }

   
    

 
}
