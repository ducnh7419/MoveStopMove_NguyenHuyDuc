using System;
using System.Collections;
using System.Collections.Generic;
using GloabalEnum;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class UICWeaponSkinScrollView : MonoBehaviour
{
    private UnityAction unityAction;
    public int id;
    private UICShopWeapon uICShopWeapon;
    [NonSerialized] public int SelectedId;
    private WeaponData weaponData;
    public Transform SkinTabGO;
    public UICWeaponSkinScrollViewItem UICWeaponSkinScrollViewPrefab;
    private List<UICWeaponSkinScrollViewItem> scrollViewItems= new();

    public void Setup(int id,WeaponData weaponData,UICShopWeapon uICShopWeapon){
        List<WeaponSkinData> weaponSkins= weaponData.Skins;
        for(int i = 0;i<weaponSkins.Count;i++){
            UICWeaponSkinScrollViewItem weapSkinTab=Instantiate(UICWeaponSkinScrollViewPrefab,SkinTabGO);
            weapSkinTab.buttonGO.onClick.AddListener(()=>OnBtnClicked(weapSkinTab.Id));
            weapSkinTab.Setup(i,weaponSkins[i]);
            scrollViewItems.Add(weapSkinTab);
            this.uICShopWeapon=uICShopWeapon;
            this.weaponData=weaponData;
        }
    }

    public void SetAction(UnityAction unityAction){
        this.unityAction+=unityAction;
    }

    public void OnBtnClicked(int id){
        SelectedId=id;
        for(int i=0;i<scrollViewItems.Count;i++){
            if(id==scrollViewItems[i].Id){
                scrollViewItems[i].ChangeSelectState(true);
            }else{
                scrollViewItems[i].ChangeSelectState(false);
            }
        }
        WeaponSkinData skinData=UserDataManager.Ins.GetWeaponSkinDataById(id,weaponData);
        uICShopWeapon.SetWeaponImage(skinData.Icon);
        unityAction();
    }
}
