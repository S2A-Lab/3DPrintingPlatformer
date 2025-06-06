using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public void Kill()
    {
        GetComponent<PlayerMovement>().enabled = false;
        Invoke(nameof(ReloadScene), 0f);
    }
    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
