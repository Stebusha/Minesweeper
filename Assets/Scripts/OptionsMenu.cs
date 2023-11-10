using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Dropdown resolutionDropdown;
    [SerializeField] private AudioMixer audioMixer;
    Resolution[] resolutions;
    [SerializeField] private Toggle toggleSound;
    [SerializeField] private Toggle toggleFullscreen;
    [SerializeField] private Slider slider;
    [SerializeField] private Dropdown qualityDropdown;
    
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }

    public void Sound(bool isSound)
    {
        AudioListener.pause = !isSound;
    }

    private void Awake()
    {
        int currentResolutionIndex = 0;
        resolutions=Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x"+resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }

        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    private void Update()
    {
        PlayerPrefs.SetFloat("Volume", slider.value);
        bool isSound = toggleSound.isOn;
        bool isFullscreen = toggleFullscreen.isOn;
        if (isSound)
            PlayerPrefs.SetInt("IsVolume", 1);
        else
            PlayerPrefs.SetInt("IsVolume", 0);
        if (isFullscreen)
            PlayerPrefs.SetInt("IsFullscreen", 1);
        else
            PlayerPrefs.SetInt("IsFullscreen", 0);
        PlayerPrefs.SetInt("Quality",qualityDropdown.value);
        PlayerPrefs.Save();
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("IsVolume") && PlayerPrefs.HasKey("IsFullscreen")&&PlayerPrefs.HasKey("Quality"))
        {
            slider.value = PlayerPrefs.GetFloat("Volume");
            if (PlayerPrefs.GetInt("IsVolume") == 1)
                toggleSound.isOn = true;
            else
                toggleSound.isOn = false;
            if (PlayerPrefs.GetInt("IsFullscreen") == 1)
                toggleFullscreen.isOn = true;
            else
                toggleFullscreen.isOn = false;
            qualityDropdown.value = PlayerPrefs.GetInt("Quality");
        }
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Quality", qualityIndex);
        PlayerPrefs.Save();
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
