using UnityEngine;

public class Collectable : MonoBehaviour
{
    public MatType matType = MatType.FILAMENT; 
    public PlayerInventory inventory;

    void Awake()
    {
        inventory = GameObject.FindWithTag("GameController").GetComponent<PlayerInventory>();
    }
     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inventory.AddMaterial(matType, 100);
            Destroy(this.gameObject);
        }
    }
}
