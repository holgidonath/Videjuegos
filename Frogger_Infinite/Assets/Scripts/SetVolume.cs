using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;

    void Start()
    {
        LoadValues();
    }
    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("Volume",sliderValue);
    }
    void LoadValues()
    {
    
    }
}
