using UnityEngine;

public class PrintUiToggler : MonoBehaviour
{

    public UiManager uiManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            uiManager.TogglePrintUI();
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
         if (other.CompareTag("Player"))
        {
            uiManager.TogglePrintUI();
        }
    }
}
