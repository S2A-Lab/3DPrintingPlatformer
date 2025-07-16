using UnityEngine;

public class MetalBLock : MonoBehaviour
{
    [SerializeField] private float floatRadius = 10f; // Distance at which floaty behavior activates
    [SerializeField] private LayerMask playerLayer;

    private GameObject player;
    private PlayerMovement playerMovement;
    private bool isFloating = false;

    private void Start()
    {
        // Find the player by tag or assign manually
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }

  private void Update()
{
    BoxCollider2D box = GetComponent<BoxCollider2D>();
   
    
    if (box == null || player == null || playerMovement == null) return;

    float distance = Vector2.Distance(transform.position, player.transform.position);

    if (distance <= floatRadius && !isFloating)
    {
        isFloating = true;
        playerMovement.StartFloat();
    }
    else if (distance > floatRadius && isFloating)
    {
        isFloating = false;
        playerMovement.StopFloat();
    }

    if (isFloating)
    {
        // Apply gentle magnetic pull toward this block
        Vector2 directionToBlock = (Vector2)(transform.position - player.transform.position).normalized;
        float pullForce = 2f; // You can tweak this value for feel

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(directionToBlock * pullForce);
        }
    }
}


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, floatRadius);
    }
}
