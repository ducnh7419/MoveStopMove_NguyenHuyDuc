using System.Collections;
using System.Collections.Generic;
using GloabalEnum;
using UnityEngine;

public class UICLose : UICResult
{
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
