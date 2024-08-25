using System.Collections;
using System.Collections.Generic;
using GloabalEnum;
using TMPro;
using UnityEngine;

public class UICLose : UICResult
{
    public TextMeshProUGUI textLose;

    protected override void  Start() {
        base.Start();
        textLose.SetText("You are at {0}th Place",LevelManager.Ins.GetNORemainBots()+1);
    }
    protected override void OnEnable() {
        base.OnEnable();
        if(UserDataManager.Ins.CanPlayerBeRevived()){
            ChangeGiftState(false);
            btnAds.gameObject.SetActive(true);
        }else{
            ChangeGiftState(true);
            btnAds.gameObject.SetActive(false);
        }
        
    }

    protected override void OnAdsClick()
    {
        SoundManager.Ins.PlaySFX(ESound.CLICK);
        UserDataManager.Ins.ChangeBudget(-coin);
        LevelManager.Ins.RevivePlayer();     
        GameManager.Ins.SetGameResult(GloabalEnum.EGameResult.None);
        GameManager.Ins.ChangeState(GameManager.State.OngoingGame);
        Time.timeScale=1;
        Close(0f);
    }
}
