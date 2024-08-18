
using GloabalEnum;
using UnityEngine.UI;

public class UICMainMenu : UICanvas
{
   public Button BtnPlay;
   public Button BtnShopWeapon;
   public Button btnSettings;

   public Button BtnShopSkin;

   private void Start() {
      BtnPlay.onClick.AddListener(OnBtnPlayClick);
      BtnShopWeapon.onClick.AddListener(OnBtnShopWeaponClick);
      BtnShopSkin.onClick.AddListener(OnBtnShopSkinClick);
      btnSettings.onClick.AddListener(OnBtnSettingsClick);
   }

    private void OnBtnSettingsClick()
    {
        UIManager.Ins.OpenUI<UICMainMenuSettings>();
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
