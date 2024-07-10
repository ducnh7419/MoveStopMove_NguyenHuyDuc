using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICMainMenu : MonoBehaviour
{
   public Button BtnPlay;
   public Button BtnShopWeapon;

   public Button BtnShopSkin;

   private void Start() {
      BtnPlay.onClick.AddListener(()=>GameManager.Ins.ChangeState(GameManager.State.StartGame));
      BtnShopWeapon.onClick.AddListener(()=>GameManager.Ins.ChangeState(GameManager.State.WeaponShop));
      BtnShopSkin.onClick.AddListener(()=>GameManager.Ins.ChangeState(GameManager.State.SkinShop));
   }
}
