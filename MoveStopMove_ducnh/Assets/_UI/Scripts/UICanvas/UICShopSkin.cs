using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICShopSkin : UICanvas
{
   public Button closeButton;
   
   private void Start() {
        closeButton.onClick.AddListener(OnClose);
   }

    private void OnClose()
    {
        GameManager.Ins.ChangeState(GameManager.State.MainMenu);
        UserDataManager.Ins.LoadAllEquippedItem();
    }
}
