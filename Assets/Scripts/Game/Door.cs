using UnityEngine;
using System.Collections.Generic;
using System.Collections; 
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    SceneController sceneController;
    public Animator sceneTransitions;
    public float time = 1f;
    public GameObject interactableIcon;

    void Start()
    {
        interactableIcon.SetActive(false);
    }

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        LoadNextLevel();
    }
    public void ToggleInteractIcon(bool isOn)
    {
      interactableIcon.SetActive(isOn); 
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        sceneTransitions.SetTrigger("Start");
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(levelIndex);
    }

   
}
