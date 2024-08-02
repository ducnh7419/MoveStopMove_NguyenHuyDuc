using System;
using System.Collections;
using System.Collections.Generic;
using GloabalEnum;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UICInGameSettings : UICanvas
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] Button closeBtn;

    private void Start() {
        masterVolumeSlider.onValueChanged.AddListener(delegate {OnMasterVolumeSliderValueChanged();});
        sfxVolumeSlider.onValueChanged.AddListener(delegate {OnSFXVolumeSliderValueChanged();});
        closeBtn.onClick.AddListener(OnCloseBtnClicked);
        Tuple<float,float> soundSettings=UserDataManager.Ins.GetVolumeSettings();
        masterVolumeSlider.value=soundSettings.Item1;
        sfxVolumeSlider.value=soundSettings.Item2;
        SetMasterVolume(soundSettings.Item1);
        SetSfxVolume(soundSettings.Item2);

    }

    private void OnCloseBtnClicked()
    {
        SoundManager.Ins.PlaySFX(ESound.CLICK);
        UserDataManager.Ins.SaveVolumeSettings(masterVolumeSlider.value,sfxVolumeSlider.value);
        UIManager.Ins.OpenUI<UICPauseMenu>();
        this.Close(0f);
    }

    private void OnSFXVolumeSliderValueChanged()
    {
        SetMasterVolume(masterVolumeSlider.value);
    }

    private void OnMasterVolumeSliderValueChanged()
    {
        SetSfxVolume(sfxVolumeSlider.value);
    }

    public void SetMasterVolume(float level){
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(level)*20f);
    }

    public void SetSfxVolume(float level){
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(level)*20f);
    }
}
