using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Animator sceneTransitions;
    public float time = 1f;


    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        sceneTransitions.SetTrigger("LevelEnd");
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(levelIndex);
    }
}
