using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;

    public AudioClip music;

    void Start()
    {
        musicSource.clip = music;
        musicSource.Play();
    }

}
