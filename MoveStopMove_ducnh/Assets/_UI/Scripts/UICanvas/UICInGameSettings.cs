using System;
using GloabalEnum;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UICInGameSettings : UICanvas
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider bgVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] Button closeBtn;

    protected virtual void Start() {
        masterVolumeSlider.onValueChanged.AddListener(delegate {OnMasterVolumeSliderValueChanged();});
        sfxVolumeSlider.onValueChanged.AddListener(delegate {OnSFXVolumeSliderValueChanged();});
        bgVolumeSlider.onValueChanged.AddListener(delegate {OnBgVolumeSliderValueChanged();});
        closeBtn.onClick.AddListener(OnCloseBtnClicked);
        Tuple<float,float,float> soundSettings=UserDataManager.Ins.GetVolumeSettings();
        masterVolumeSlider.value=soundSettings.Item1;
        bgVolumeSlider.value=soundSettings.Item2;
        sfxVolumeSlider.value=soundSettings.Item3;
        SetMasterVolume(soundSettings.Item1);
        SetBackGroundVolume(soundSettings.Item2);
        SetSfxVolume(soundSettings.Item3);
    }

    private void OnBgVolumeSliderValueChanged()
    {
        SetBackGroundVolume(bgVolumeSlider.value);
    }

    protected virtual void OnCloseBtnClicked()
    {
        SoundManager.Ins.PlaySFX(ESound.CLICK);
        UserDataManager.Ins.SaveVolumeSettings(masterVolumeSlider.value,bgVolumeSlider.value,sfxVolumeSlider.value);
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

    public void SetBackGroundVolume(float level){
        audioMixer.SetFloat("BackgroundVolume", Mathf.Log10(level)*20f);
    }
}
