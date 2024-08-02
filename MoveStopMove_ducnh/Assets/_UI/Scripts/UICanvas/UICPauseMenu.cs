using System;
using System.Collections;
using System.Collections.Generic;
using GloabalEnum;
using UnityEngine;
using UnityEngine.UI;

public class UICPauseMenu : UICanvas
{
    [SerializeField] private Button btnContinue;
    [SerializeField] private Button btnRetry;
    [SerializeField] private Button btnMainMenu;
    [SerializeField] private Button btnSettings;

    private void Start() {
        btnContinue.onClick.AddListener(OnBtnContinueClicked);
        btnRetry.onClick.AddListener(OnBtnRetryClicked);
        btnMainMenu.onClick.AddListener(OnBtnMainMenuClicked);
        btnSettings.onClick.AddListener(OnBtnSettingsClicked);
    }

    private void OnBtnSettingsClicked()
    {
        SoundManager.Ins.PlaySFX(ESound.CLICK);
        UIManager.Ins.OpenUI<UICInGameSettings>();
        this.Close(0f);
        
    }

    private void OnBtnMainMenuClicked()
    {
        SoundManager.Ins.PlaySFX(ESound.CLICK);
        LevelManager.Ins.ResetLevel();
        GameManager.Ins.ChangeState(GameManager.State.MainMenu);
        Time.timeScale=1;
    }

    private void OnBtnRetryClicked()
    {
        SoundManager.Ins.PlaySFX(ESound.CLICK);
        Time.timeScale=1;
        this.Close(0.5f);
        LevelManager.Ins.ResetLevel();
        StartCoroutine(GameManager.Ins.DelayChangeState(GameManager.State.StartGame,.5f));
    }

    private void OnBtnContinueClicked()
    {
        SoundManager.Ins.PlaySFX(ESound.CLICK);
        this.Close(0f);
        Time.timeScale=1;
    }
}
