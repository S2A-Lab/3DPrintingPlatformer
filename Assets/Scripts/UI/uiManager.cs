using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TMP_Text filamentText;
    public TMP_Text metalText; 
    PlayerInventory inventory;
    void Start()
    {
        filamentText.text = "0";
        metalText.text = "0";
    }


    public void UpdateText(int filament, int metal)
    {
        filamentText.text = "" + filament;
        metalText.text = "" + metal;
    }
    
}
