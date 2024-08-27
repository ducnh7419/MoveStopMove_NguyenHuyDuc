
using System;
using DG.Tweening;
using GloabalEnum;
using UnityEngine;
using UnityEngine.UI;

public class UICMainMenu : UICanvas
{
   public Button BtnPlay;
   public Button BtnShopWeapon;
   public Button btnSettings;
   public Button muteSoundBtn;

   public GameObject muteImage;
   public GameObject unmuteImage;
   

   public Transform leftGroup;
   public Transform rightGroup;

   public Button BtnShopSkin;



   private void Start()
   {
      BtnPlay.onClick.AddListener(OnBtnPlayClick);
      BtnShopWeapon.onClick.AddListener(OnBtnShopWeaponClick);
      BtnShopSkin.onClick.AddListener(OnBtnShopSkinClick);
      btnSettings.onClick.AddListener(OnBtnSettingsClick);
      muteSoundBtn.onClick.AddListener(OnMuteSoundBtnClick);
      SetMuteImageStatus();
   }

    private void OnMuteSoundBtnClick()
    {
      SoundManager.Ins.Mute(!SoundManager.Ins.IsMute);
      SetMuteImageStatus();
    }

    private void OnEnable() {
      leftGroup.DOMoveX(leftGroup.position.x-500,.5f).From();
      rightGroup.DOMoveX(rightGroup.position.x+500,.5f).From();
      SetMuteImageStatus();
   }

   private void SetMuteImageStatus(){
      if(SoundManager.Ins.IsMute){
         unmuteImage.SetActive(true);
         muteImage.SetActive(false);
      }else{
         unmuteImage.SetActive(false);
         muteImage.SetActive(true);
      }
   }

   private void OnBtnSettingsClick()
   {
      UIManager.Ins.OpenUI<UICMainMenuSettings>();
   }

   private void OnBtnPlayClick()
   {
      SoundManager.Ins.PlaySFX(ESound.CLICK);
      ButtonMoving();
      StartCoroutine(GameManager.Ins.DelayChangeState(GameManager.State.StartGame,.5f));
   }

   private void ButtonMoving(){
      leftGroup.DOMoveX(leftGroup.position.x-500,.5f).OnComplete(()=>leftGroup.DOMoveX(leftGroup.position.x+500,.5f));
      rightGroup.DOMoveX(rightGroup.position.x+500,.5f).OnComplete(()=>rightGroup.DOMoveX(rightGroup.position.x-500,.5f));
      
   }

   private void OnBtnShopWeaponClick()
   {
      SoundManager.Ins.PlaySFX(ESound.CLICK);
      GameManager.Ins.ChangeState(GameManager.State.WeaponShop);
   }

   private void OnBtnShopSkinClick()
   {
      SoundManager.Ins.PlaySFX(ESound.CLICK);
      GameManager.Ins.ChangeState(GameManager.State.SkinShop);
   }
}
