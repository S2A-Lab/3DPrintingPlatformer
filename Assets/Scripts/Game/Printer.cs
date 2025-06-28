using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Printer : MonoBehaviour {

    public PrinterUIController printerUI; 
    public Queue<BlockData> printQueue = new Queue<BlockData>();
    public List<BlockData> finishedPrints = new List<BlockData>();
    public PlayerInventory inventory; 

    private bool isPrinting = false;


    public void AddToPrintQueue(BlockData block)
    {
        if (!inventory.BuyPrint(block)) return;
        printQueue.Enqueue(block);
         printerUI.PushUIQueue(block);


        if (!isPrinting)
            StartCoroutine(PrintNext());
    }

    private IEnumerator PrintNext()
    {
        while (printQueue.Count > 0)
        {
            isPrinting = true;
            BlockData currentBlock = printQueue.Dequeue();
            Debug.Log($"Printing {currentBlock.blockName}, takes {currentBlock.printTime} seconds...");

            yield return new WaitForSeconds(currentBlock.printTime);

            finishedPrints.Add(currentBlock);
            printerUI.PopUIQueue(); 
            printerUI.PushUITray(currentBlock); 
            
            
            Debug.Log($"{currentBlock.blockName} finished printing!");
        }

        isPrinting = false;
    }

    public List<BlockData> GetFinishedPrints()
    {
        return finishedPrints;
    }

   public void ClearFinishedPrints()
{
    finishedPrints.Clear();
    printerUI.ClearUITray();
}
}
