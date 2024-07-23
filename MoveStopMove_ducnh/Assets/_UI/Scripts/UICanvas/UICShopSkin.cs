using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICShopSkin : UICanvas
{
   public TextMeshProUGUI Budget;
   public Button closeButton;
   
   private void Start() {
        Budget.text=UserDataManager.Ins.GetCurrentBudget().ToString();
        closeButton.onClick.AddListener(OnClose);
   }

    private void OnClose()
    {
        GameManager.Ins.ChangeState(GameManager.State.MainMenu);
    }
}
