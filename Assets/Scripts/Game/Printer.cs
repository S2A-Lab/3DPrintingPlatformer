using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Printer : MonoBehaviour
{

    public PrinterUIController printerUI;
    public Queue<BlockData> printQueue = new Queue<BlockData>();
    private PlayerInventory inventory;

    private bool isPrinting = false;
    void Awake()
    {
        inventory = GameObject.FindWithTag("GameController").GetComponent<PlayerInventory>();
    }


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
            printerUI.RunCurrentPrint(currentBlock.printTime, currentBlock);

            yield return new WaitForSeconds(currentBlock.printTime);


            printerUI.PopUIQueue();
            inventory.AddBlock(currentBlock);


            Debug.Log($"{currentBlock.blockName} finished printing!");
        }

        isPrinting = false;
        printerUI.ResetCurrentPrint();
    }



}
