using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICResult : UICanvas
{
    [SerializeField] protected GameObject closeGift;
    [SerializeField] protected GameObject openGift;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] Button btnRetry;
    [SerializeField] protected Button btnMainMenu;
    [SerializeField] protected Button btnAds;
    protected int coin;

    protected virtual void OnEnable() {
        ChangeGiftState(false);
        coin=UserDataManager.Ins.GetPlayerCurrentCoin();
        UserDataManager.Ins.ChangeBudget(coin);
        coinText.text=coin.ToString();
    }


    private void Start() {
        btnAds.onClick.AddListener(OnAdsClick);
        btnRetry.onClick.AddListener(OnBtnRetryClicked);
        btnMainMenu.onClick.AddListener(OnBtnMainMenuClicked);
    }

    protected virtual void OnAdsClick()
    {
        ChangeGiftState(true);
        UserDataManager.Ins.ChangeBudget(coin);
        coinText.text=(coin*2).ToString();
    }

    protected void ChangeGiftState(bool open){
        if(open){
            openGift.SetActive(true);
            closeGift.SetActive(false);
        }else{
            openGift.SetActive(false);
            closeGift.SetActive(true);
        }
    }

    protected virtual void OnBtnMainMenuClicked()
    {
        LevelManager.Ins.DestroyLevel();
        GameManager.Ins.ChangeState(GameManager.State.MainMenu);
        Time.timeScale=1;
    }

    protected virtual void OnBtnRetryClicked()
    {
        Time.timeScale=1;
        this.Close(0.5f);
        LevelManager.Ins.RestartLevel();
    }
}
