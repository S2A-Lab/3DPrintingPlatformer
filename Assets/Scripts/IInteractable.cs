using UnityEngine;

public interface IInteractable
{
    void Interact();

    void ToggleInteractIcon(bool isON); 
    bool CanInteract(); 
}
