using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collectable : MonoBehaviour
{
    public MatType matType = MatType.FILAMENT;
    public int amount = 100; 
    public PlayerInventory inventory;

    [Header("Idle Animation Settings")]
    public float floatAmplitude = 0.1f;
    public float floatFrequency = 1f;
    public float rotationSpeed = 0f; // Set to >0 if you want rotation

    private Vector3 startPos;

    void Awake()
    {
        inventory = GameObject.FindWithTag("GameController").GetComponent<PlayerInventory>();
        startPos = transform.position;
    }

    void Update()
    {
        AnimateIdle();
    }

    void AnimateIdle()
    {
        transform.position = startPos + Vector3.up * Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;

        if (rotationSpeed != 0f)
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inventory.AddMaterial(matType, amount);
            Destroy(gameObject);
        }
    }
}
