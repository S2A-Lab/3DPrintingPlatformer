using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuController : MonoBehaviour
{
    private VisualElement ui;
    private Button playButton;
    private Button optionsButton;
    private Button exitButton;

    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnEnable()
    {
        playButton = ui.Q<Button>("PlayButton");
        optionsButton = ui.Q<Button>("SettingsButton");
        exitButton = ui.Q<Button>("ExitButton");

        if (playButton != null)
            playButton.clicked += OnPlayButtonClicked;

        if (optionsButton != null)
            optionsButton.clicked += OnSettingsButtonClicked;

        if (exitButton != null)
            exitButton.clicked += OnExitButtonClicked;
    }

    private void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void OnSettingsButtonClicked()
    {
        Debug.Log("Settings button clicked.");
    
    }

    private void OnExitButtonClicked()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
