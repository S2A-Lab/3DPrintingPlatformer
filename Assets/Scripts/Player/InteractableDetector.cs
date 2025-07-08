using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableDetector : MonoBehaviour
{
    IInteractable interactableInRange = null;



  public void OnInteract(InputAction.CallbackContext context)
{
    Debug.Log($"Try To Interact - phase: {context.phase}"); 
    if (context.performed)
    {
        Debug.Log("Performing interact attempt...");
        interactableInRange?.Interact();
    }
}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            Debug.Log($"Entered trigger with: {collision.gameObject.name}");
            interactableInRange = interactable;
            interactable.ToggleInteractIcon(true); 
           
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
        {
            interactableInRange = null;
            interactable.ToggleInteractIcon(false); 
        }
    }
}
