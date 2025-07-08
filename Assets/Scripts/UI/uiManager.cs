using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Inventory UI")]
    public TMP_Text inv_filamentText;
    public TMP_Text inv_metalText;
    public TMP_Text inv_rubberText;
    public TMP_Text inv_resinText;
    public TMP_Text inv_blocks_filamentText;
    public TMP_Text inv_blocks_metalText;
    public TMP_Text inv_blocks_rubberText;
    public TMP_Text inv_blocks_resinText;

    [Header("Toggler")]

    [SerializeField] private GameObject uiToToggle;

    private PlayerInventory inventory;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        uiToToggle.SetActive(false);
        UpdateInventory(0, 0, 0, 0);
        UpdateMaterialsInventory(0, 0, 0, 0);
    }

    public void TogglePrintUI()
    {
        bool isActive = uiToToggle.activeSelf;
        uiToToggle.SetActive(!isActive);
    }


    public void UpdateInventory(int filament, int metal, int rubber, int resin)
    {
        inv_filamentText.text = "" + filament;
        inv_metalText.text = "" + metal;
        inv_rubberText.text = "" + rubber;
        inv_resinText.text = "" + resin;

    }
    public void UpdateMaterialsInventory(int filament, int metal, int rubber, int resin)
    {
        inv_blocks_filamentText.text = "" + filament;
        inv_blocks_metalText.text = "" + metal;
        inv_blocks_rubberText.text = "" + rubber;
        inv_blocks_resinText.text = "" + resin;

    }
    
}
