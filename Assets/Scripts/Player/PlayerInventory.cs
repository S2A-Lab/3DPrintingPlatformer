using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    public int filament = 0;
    public int metal = 0;
    public UiManager uimanager;
    public void AddFilament(int amount)
    {
        filament += amount;
        uimanager.UpdateText(filament, metal);
    }
    public void AddMetal(int amount)
    {
        metal += amount;
        uimanager.UpdateText(filament,metal);
    }
    
  
    
}
