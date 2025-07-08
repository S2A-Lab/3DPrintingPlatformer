using UnityEngine;

public class PlasticBlock : MonoBehaviour
{
    [SerializeField] private int maxSteps = 3;  // Number of times the block can be stepped on
    private int stepCount = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            stepCount++;

            if (stepCount >= maxSteps)
            {
                Destroy(gameObject);
            }
        }
    }
}
