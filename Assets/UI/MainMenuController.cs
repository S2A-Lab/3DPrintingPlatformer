using UnityEngine;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuController : MonoBehaviour
{
    public VisualElement ui;
    public Button playButton;
    public Button optionsButton;
    public Button saveQuitButton;

    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnEnable()
    {
        playButton = ui.Q<Button>("PlayButton");
        playButton.clicked += OnPlayButtonClicked;

        optionsButton = ui.Q<Button>("OptionsButton");
        optionsButton.clicked += OnOptionsButtonClicked;

        saveQuitButton = ui.Q<Button>("SaveExitButton");
        saveQuitButton.clicked += OnSaveQuitButtonClicked;
    }

    private void OnSaveQuitButtonClicked()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }

    private void OnOptionsButtonClicked()
    {
        Debug.Log("Options");
    }

    private void OnPlayButtonClicked()
    {
        gameObject.SetActive(false);
    }
}
