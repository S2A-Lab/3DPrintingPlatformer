using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int filament = 0;
    public int powder = 0;
    public int resin = 0; 


    public void IncreaseFilament(int amount)
    {
        filament += amount;
    }
    public void IncreasePowder(int amount)
    {
        powder += amount;
    }
       public void IncreaseResin(int amount)
    {
       resin += amount; 
    }
}
