using System.Collections;
using System.Collections.Generic;
using GloabalEnum;
using UnityEngine;
using UnityEngine.UI;

public class UICMainMenu : UICanvas
{
   public Button BtnPlay;
   public Button BtnShopWeapon;

   public Button BtnShopSkin;

   private void Start() {
      BtnPlay.onClick.AddListener(OnBtnPlayClick);
      BtnShopWeapon.onClick.AddListener(OnBtnShopSkinClick);
      BtnShopSkin.onClick.AddListener(OnBtnShopSkinClick);
   }

   private void OnBtnPlayClick(){
      SoundManager.Ins.PlaySFX(ESound.CLICK);
      GameManager.Ins.ChangeState(GameManager.State.StartGame);
   }

   private void OnBtnShopWeaponClick(){
      SoundManager.Ins.PlaySFX(ESound.CLICK);
      GameManager.Ins.ChangeState(GameManager.State.WeaponShop);
   }

   private void OnBtnShopSkinClick(){
      SoundManager.Ins.PlaySFX(ESound.CLICK);
      GameManager.Ins.ChangeState(GameManager.State.SkinShop);
   }
}
