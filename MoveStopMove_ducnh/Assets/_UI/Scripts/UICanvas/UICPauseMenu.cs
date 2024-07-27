using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICPauseMenu : UICanvas
{
    [SerializeField] private Button btnContinue;
    [SerializeField] private Button btnRetry;

    private void Start() {
        btnContinue.onClick.AddListener(OnBtnContinueClicked);
        btnRetry.onClick.AddListener(OnBtnRetryClicked);
    }

    private void OnBtnRetryClicked()
    {
        Time.timeScale=1;
        LevelManager.Ins.RestartLevel();
        
    }

    private void OnBtnContinueClicked()
    {
        this.Close(0.5f);
        Time.timeScale=1;
    }
}
