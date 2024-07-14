using System;
using System.Collections;
using System.Collections.Generic;
using GloabalEnum;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
   private static UserDataManager ins;
   public static UserDataManager Ins=>ins;
   private Player player;
   public Player Player { get => player; set => player = value; }


    public bool CheckPurchasedItem(int id,EItemType eItemType)
    {
        if(PlayerPrefs.GetInt(GlobalDictionary.IdPrefix[eItemType])!=0)
            return true;
        return false;
    }

    private string GetKey(EItemType eItemType){
        string key="";
        switch(eItemType){
            case EItemType.Hair:
                key="Hair";
                return key;
            case EItemType.Pant:
                key="Pant";
                return key;
            case EItemType.Shield:
                key="Shield";
                return key;
            case EItemType.FullSet:
                key="Full Set";
                return key;
            case EItemType.None:
                key="";
                return key;
        }
        return key;
    }

    public bool CheckCurrentEquippedItem(int id,EItemType eItemType){
        string key=GetKey(eItemType);
        int val=PlayerPrefs.GetInt(key);
        if(--val==id){
            return true;
        }
        return false;
    }

    public void EquipItem(int id,EItemType eItemType){
        string key=GetKey(eItemType);
        PlayerPrefs.SetInt(key, ++id);
    }

    public void UnEquipItem(EItemType eItemType){
        string key=GetKey(eItemType);
        PlayerPrefs.SetInt(key,0);
    }

    public void ChangePlayerHair(int id){
        
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="eItemType"></param>
    /// <returns>true when success buying</returns>
    public bool PurchaseItem(int id,EItemType eItemType){
        ItemData itemData=GameManager.Ins.ItemDataConfigSO.GetItemData(eItemType,id);
        if(player.Coin>itemData.Price){
            PlayerPrefs.SetInt(GlobalDictionary.IdPrefix[eItemType]+id,1);
            return true;
        }
        return false;
    }
    
    // public void SetSkin(string id){
    //     
    // }
}
