using UnityEngine;
using System.Collections.Generic;
using System.Collections; 
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour, IInteractable
{
    SceneController sceneController;
    public Animator sceneTransitions;
    public float time = 1f;
    public GameObject interactableIcon;
    bool isDoor; 
    public AudioClip soundClip; // Assign in Inspector
    public AudioSource audioSource;


    void Start()
    {
        interactableIcon.SetActive(false);
        audioSource.clip = soundClip;
    }

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        audioSource.Play(); 
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
