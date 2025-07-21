using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip music;
    public AudioClip hoverButton;
    public AudioClip clickButton;

    void Start()
    {
        musicSource.clip = music;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayHover()
    {
        Debug.Log("hoverSound");
        sfxSource.PlayOneShot(hoverButton);
    }

    public void PlayClick()
    {
        Debug.Log("clickSound");
        sfxSource.PlayOneShot(clickButton);
    }
}
