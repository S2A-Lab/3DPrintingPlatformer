using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections.Generic;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    Resolution[] resolutions;
    public TMP_Dropdown resDropdown;

    public GameObject optionsPanel;

    void Start()
    {
        // Set default volumes
        SetMusicVolume(-30f); // dB
        SetSFXVolume(-10f);   // dB

        // Setup resolution dropdown
        int currResIndex = 0;
        resolutions = Screen.resolutions;
        resDropdown.ClearOptions();

        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currResIndex = i;
            }
        }

        resDropdown.AddOptions(options);
        resDropdown.value = currResIndex;
        resDropdown.RefreshShownValue();
    }

    // OLD method - optional: remove if unused
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void ToggleOptions(bool isOn)
    {
        Debug.Log("toggleOptions"); 
        optionsPanel.SetActive(isOn);
    }
}
