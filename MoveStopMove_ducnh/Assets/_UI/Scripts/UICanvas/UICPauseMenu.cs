using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICPauseMenu : UICanvas
{
    [SerializeField] private Button btnContinue;
    [SerializeField] private Button btnRetry;
    [SerializeField] private Button btnMainMenu;

    private void Start() {
        btnContinue.onClick.AddListener(OnBtnContinueClicked);
        btnRetry.onClick.AddListener(OnBtnRetryClicked);
        btnMainMenu.onClick.AddListener(OnBtnMainMenuClicked);
    }

    private void OnBtnMainMenuClicked()
    {
        LevelManager.Ins.DestroyLevel();
        GameManager.Ins.ChangeState(GameManager.State.MainMenu);
        Time.timeScale=1;
    }

    private void OnBtnRetryClicked()
    {
        Time.timeScale=1;
        this.Close(0.5f);
        LevelManager.Ins.RestartLevel();
        
    }

    private void OnBtnContinueClicked()
    {
        this.Close(0.5f);
        Time.timeScale=1;
    }
}
