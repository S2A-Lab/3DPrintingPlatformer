using UnityEngine;

public class Collectable : MonoBehaviour
{
    public enum Type
    {
        FILAMENT,
        POWDER, 
        RESIN
        
    }
    public Type type = Type.FILAMENT; 

     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            switch (type)
            {
                case Type.FILAMENT:
                    other.GetComponent<PlayerInventory>().IncreaseFilament(1);
                    break;
                case Type.POWDER:
                    other.GetComponent<PlayerInventory>().IncreasePowder(1);
                    break;
                case Type.RESIN:
                    other.GetComponent<PlayerInventory>().IncreaseResin(1);
                    break;

            }
          Destroy(this.gameObject);

          
        }
    }
}
