using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    [SerializeField]private AudioMixer mixer;
    [SerializeField]private Slider slider;

    public void setLevel(float sliderValue)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
    }

    void Awake()
    {
        float value;
        mixer.GetFloat("MasterVolume", out value);
        value = Mathf.Pow(value/20, 10);
        slider.value = value;
        Debug.Log(value);
    }
}

