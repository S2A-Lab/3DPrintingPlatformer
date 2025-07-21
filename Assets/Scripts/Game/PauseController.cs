using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static bool IsGamePaused { get; private set; } = false;
    public GameObject pauseMenu;



    public void TogglePause()
    {
        if (IsGamePaused)
        {
            Resume();
        }
        else
        {
            Pause(); 
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        IsGamePaused = false; 
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        IsGamePaused = true; 
    }




    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }



    public void OnQuit()
{
    Application.Quit();

#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#endif
}
    


}
