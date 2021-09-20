using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;
    public AudioMixer audioMixer;
    public Slider slider;
    private float volume;
    void Start()
    {
    
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        string option1 = "320 x 200";
        string option2 = "640 x 480";
        options.Add(option1);
        options.Add(option2);
        resolutionDropdown.AddOptions(options);
       
        if (Screen.width == 320)
        {
            resolutionDropdown.value = 0;
        }
        else
        {
            resolutionDropdown.value = 1;
        }
        resolutionDropdown.RefreshShownValue();
        audioMixer.GetFloat("Volume",out volume);
        slider.value = volume;
    }


    public void SetResolution(int resolutionIndex)
    {
        if (resolutionIndex == 0)
        {
        Screen.SetResolution(320, 200,false);
        }
        else 
        {
        Screen.SetResolution(640, 480,false); 
        }
    }


    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("Volume",volume);
        
    }

}
