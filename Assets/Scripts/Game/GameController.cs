using UnityEngine;

public class GameController : MonoBehaviour
{
    public PrinterUIController printerUI; 
    public void UnlockMaterial(MatType type)
    {
      printerUI.UnlockButton(type);
    }
}
