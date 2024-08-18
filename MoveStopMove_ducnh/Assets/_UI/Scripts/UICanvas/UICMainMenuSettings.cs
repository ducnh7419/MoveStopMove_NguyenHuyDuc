using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICMainMenuSettings : UICInGameSettings
{
   public Button btnChangeName;

    protected override void Start(){
        base.Start();
        btnChangeName.onClick.AddListener(OnBtnChangeNameClicked);
    }

    private void OnBtnChangeNameClicked()
    {
        UIManager.Ins.OpenUI<UICChangeName>();
        Close(0f);
    }

    protected override void OnCloseBtnClicked()
    {
        Close(0f);
    }
}
