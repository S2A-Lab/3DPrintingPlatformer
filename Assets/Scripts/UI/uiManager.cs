using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private GameObject uiToToggle;
    public TMP_Text inv_filamentText;
    public TMP_Text inv_metalText; 
    PlayerInventory inventory;
    void Start()
    {
        uiToToggle.SetActive(false);
        inv_filamentText.text = "0";
        inv_metalText.text = "0";
    }

    public void TogglePrintUI()
    {
        bool isActive = uiToToggle.activeSelf;
         uiToToggle.SetActive(!isActive);
    }


    public void UpdateInventory(int filament, int metal)
    {
        inv_filamentText.text = "" + filament;
        inv_metalText.text = "" + metal;
    }
    
}
