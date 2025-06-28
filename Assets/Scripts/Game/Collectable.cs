using UnityEngine;

public class Collectable : MonoBehaviour
{
    public enum MatType
    {
        FILAMENT, 
        METAL
    }
    public MatType matType = MatType.FILAMENT; 
     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            switch (matType)
            {
                case MatType.FILAMENT:
                    other.GetComponent<PlayerInventory>().AddFilament(100);
                    break;
                case MatType.METAL:
                    other.GetComponent<PlayerInventory>().AddMetal(100);
                    break; 
        }

        Destroy(this.gameObject);
        }
    }
}
