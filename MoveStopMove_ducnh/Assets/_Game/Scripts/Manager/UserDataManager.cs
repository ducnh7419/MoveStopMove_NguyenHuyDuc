using System;
using System.Collections;
using System.Collections.Generic;
using GloabalEnum;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
   private static UserDataManager ins;
   public static UserDataManager Ins=>ins;
   [SerializeField]private HairDataConfigSO hairDataConfigSO;
   [SerializeField]private PantDataConfig pantDataConfigSO;
   [SerializeField]private ShieldDataConfigSO shieldDataConfigSO;
   [SerializeField]private SetFullSkinDataConfigSO setFullSkinDataConfigSO;
   private Player player;
   public Player Player { get => player; set => player = value; }


    public bool CheckPurchasedItem(string id)
    {
        if(PlayerPrefs.GetInt(id)!=0){
            return true;
        }
        return false;
    }

    public void ChangePlayerHair(int id){
        
    }

    public void PurchaseItem(string id){
        PlayerPrefs.SetInt(id,1);
    }
    
    public void SetSkin(string id){
        string[]splitted_id = id.Split("-");
        switch(splitted_id[0]){
            case "H":
                Player.SetHairSkin(hairDataConfigSO.GetHairSkinByEnum((HairSkinEnum)Convert.ToInt32(splitted_id[1])));
                break;
            case "P":
                Player.SetPantSkin(pantDataConfigSO.GetPantsSkinByEnum((PantSkinEnum)Convert.ToInt32(splitted_id[1])));
                break;
            case "S":
                Player.SetShieldSkin(shieldDataConfigSO.GetShieldSkinByEnum((ShieldEnum)Convert.ToInt32(splitted_id[1])));
                break;
            // case "FS":

        }
    }
}
