using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public bool isInvinsible = false; 

    public void Kill()
    {
        if (isInvinsible)
        {
            return; 
        }
        GetComponent<PlayerMovement>().enabled = false;
        Invoke(nameof(ReloadScene), 0f);
    }
    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
