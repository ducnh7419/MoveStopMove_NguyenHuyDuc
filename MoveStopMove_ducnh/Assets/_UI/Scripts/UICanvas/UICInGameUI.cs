using System;
using System.Collections;
using System.Collections.Generic;
using GloabalEnum;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICInGameUI : UICanvas
{
    [SerializeField]private TextMeshProUGUI textNoBot;
    [SerializeField]private Button btnSettings;

    private void Start() {
        btnSettings.onClick.AddListener(OnSettingsClick);
    }

    private void OnSettingsClick()
    {
        SoundManager.Ins.PlaySFX(ESound.CLICK);
        if(Time.timeScale==1){
            Time.timeScale=0;
            UIManager.Ins.OpenUI<UICPauseMenu>();
        }else{
            UIManager.Ins.CloseUI<UICPauseMenu>();
            Time.timeScale=1;
        }
    }

    public void SetText(string text){
        textNoBot.text = text;
    }

    private void Update() {
        int noBots=LevelManager.Ins.GetNORemainBots();
        SetText(noBots.ToString());
    }
}
