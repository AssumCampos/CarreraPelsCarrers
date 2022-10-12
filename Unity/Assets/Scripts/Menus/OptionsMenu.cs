using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

    public class OptionsMenu : MonoBehaviour
{
    private Resolution[] _resolutions;
    public Dropdown resolutionDropdown;
    public AudioMixer audioMixer;
    public Slider musicSlider, sfxSlider;
    public Toggle fullScreen;
    
    private string volumeMusicPref = "Music";
    private string volumeSFXPref = "SFX";
    private string resolutionIndex = "resIndex";
    private string fullscreenPref = "fullScreenPref";

    
    public void Start()
    {
        _resolutions = Screen.resolutions;
        
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
    
        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + " x " + _resolutions[i].height;
            options.Add(option);
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = GetResolutionIndex();
        resolutionDropdown.RefreshShownValue();

        LoadData();
    }

    public int GetResolutionIndex()
    {
        _resolutions = Screen.resolutions;
        
        int iniResolutionIndex = 0;
        for (int i = 0; i < _resolutions.Length; i++)
        {
            if (_resolutions[i].width == Screen.width &&
                _resolutions[i].height == Screen.height)
            {
                iniResolutionIndex = i;
            }
        }

        return iniResolutionIndex;
    }

    public void SetResolution(int resolutionI)
    {
        _resolutions = Screen.resolutions;
        Resolution resolution = _resolutions[resolutionI];

        Screen.SetResolution(resolution.width, resolution.height, fullScreen.isOn);
    }
    public void SetMusic(float volume)
    {
        audioMixer.SetFloat("music", volume);
    }
    public void SetSFX(float volume)
    {
        audioMixer.SetFloat("sfx", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SaveMusic()
    {
        PlayerPrefs.SetFloat(volumeMusicPref, musicSlider.value);        
    }    
    public void SaveSFX()
    {
        PlayerPrefs.SetFloat(volumeSFXPref, sfxSlider.value);
    }
    
    public void SaveResolution()
    {
        //Set de la Resolución
        PlayerPrefs.SetInt(resolutionIndex, resolutionDropdown.value);
    }

    public void SaveFullScreen()
    {
        int full = BoolToInt(fullScreen.isOn);
        
        //Set de Fullscreen
        PlayerPrefs.SetInt(fullscreenPref, full);
    }
    

    public void LoadData()
    {
        musicSlider.value = PlayerPrefs.GetFloat(volumeMusicPref);
        //SetMusic(volume);
        
        //SetSFX(volume);
        sfxSlider.value = PlayerPrefs.GetFloat(volumeSFXPref);

        resolutionDropdown.value = PlayerPrefs.GetInt(resolutionIndex);
        resolutionDropdown.RefreshShownValue();
        fullScreen.isOn = IntToBool(PlayerPrefs.GetInt(fullscreenPref));
        //SetFullscreen(IntToBool(fullScreen));
        //SetResolution(resIndex);
    }
    
    private bool IntToBool (int number)
    {
        return (number == 0 ? false : true);
    }
    
    private int BoolToInt (bool number)
    {
        return (number == true ? 1 : 0);
    }
}

