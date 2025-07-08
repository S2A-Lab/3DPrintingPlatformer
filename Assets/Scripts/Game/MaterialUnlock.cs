using UnityEngine;

public class MaterialUnlock : MonoBehaviour, IInteractable
{
    public MatType matType;
    private GameController gameController;
    public GameObject interactableIcon; 

    void Awake()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    public void ToggleInteractIcon(bool isOn)
    {
       interactableIcon.SetActive(isOn); 
    }

     public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {

        gameController.UnlockMaterial(matType);
        Destroy(this.gameObject);
    }
    
}
