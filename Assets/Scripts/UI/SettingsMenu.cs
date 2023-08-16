using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [Header("Master Volume")]
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private Slider mainSlider;
    [SerializeField] private TMP_Text mainProgressText;

    [Header("BGM Volume")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private TMP_Text bgmProgressText;

    [Header("SFX Volume")]
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private TMP_Text sfxProgressText;

    // Start is called before the first frame update
    void Start()
    {
        SetMainVolume();
        SetBGMVolume();
        SetSFXVolume();
    }

    public void SetMainVolume()
    {
        float volume = mainSlider.value;
        mainMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        mainProgressText.text = Mathf.RoundToInt(volume * 100) + "%";
    }

    public void SetBGMVolume()
    {
        float volume = bgmSlider.value;
        mainMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        bgmProgressText.text = Mathf.RoundToInt(volume * 100) + "%";
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        mainMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        sfxProgressText.text = Mathf.RoundToInt(volume * 100) + "%";
    }
}
