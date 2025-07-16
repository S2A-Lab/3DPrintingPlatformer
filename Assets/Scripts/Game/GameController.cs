using UnityEngine;
using System.Collections.Generic;
using System.Collections; 
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{


  public PrinterUIController printerUI;
  public PlayerInventory inventory;
  public bool testingMode = false;

  void Start()
  {
    if (testingMode)
    {
      ToggleTestingMode();
    }
  }
  public void UnlockMaterial(MatType type)
  {
    printerUI.UnlockButton(type);
  }

  private void ToggleTestingMode()
  {
    UnlockMaterial(MatType.FILAMENT);
    UnlockMaterial(MatType.RESIN);
    UnlockMaterial(MatType.METAL);
    UnlockMaterial(MatType.RUBBER);
    inventory.AddMaterial(MatType.FILAMENT, 2000);
    inventory.AddMaterial(MatType.RESIN, 2000);
    inventory.AddMaterial(MatType.METAL, 2000);
    inventory.AddMaterial(MatType.RUBBER, 2000);


  }

  public void ReloadGame()
  {
    SceneManager.LoadScene(0);
  }
  

  public void LoadNextLevel()
  {

    if (SceneManager.GetActiveScene().buildIndex + 1 <= 5)
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
  }

  public void LoadPrevLevel()
  {
    if (SceneManager.GetActiveScene().buildIndex - 1 >= 0)
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
  }

  
}
