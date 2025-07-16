using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static bool IsGamePaused { get; private set; } = false;
    public GameObject pauseMenu;
    public GameObject optionsMenu;


    void Start()
    {
        pauseMenu.SetActive(false);
        optionsMenu = GameObject.FindWithTag("OptionsMenu");

    }
    public static void SetPause(bool pause)
    {
        IsGamePaused = pause;

    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void ToggleOptionsMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }
    


}
