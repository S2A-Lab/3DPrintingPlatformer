using UnityEngine;

public class ResinBlock : MonoBehaviour
{
    [SerializeField] private float horizontalBoost = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float direction = Mathf.Sign(rb.linearVelocity.x);
                
                // If the player isn't moving, choose a default direction (e.g., right)
                if (direction == 0)
                    direction = 1f;

                rb.AddForce(Vector2.right * direction * horizontalBoost, ForceMode2D.Impulse);
            }
        }
    }
}
