using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GloabalEnum;
using GlobalConstants;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    private static UserDataManager ins;
    public static UserDataManager Ins => ins;
    private Player player;
    private int currentBudget;
    public Player Player { get => player; set => player = value; }

    private void Awake()
    {
        if (ins != null && ins != this)
        {
            Destroy(this);
        }
        else
        {
            ins = this;
        }
    }

    #region PurchaseItem
    public string LoadAllPurchasedItem(EItemType eItemType)
    {
        switch (eItemType)
        {
            case EItemType.Hair:
                return PlayerPrefs.GetString(DataKey.PURCHASED_HAIR);
            case EItemType.Pant:
                return PlayerPrefs.GetString(DataKey.PURCHASED_PANT);
            case EItemType.Shield:
                return PlayerPrefs.GetString(DataKey.PURCHASED_SHIELD);
            case EItemType.FullSet:
                return PlayerPrefs.GetString(DataKey.PURCHASED_FS);

        }
        return "";
    }

    public void SavePurchasedItem(EItemType eItemType, int id)
    {
        List<string> ids = LoadAllPurchasedItem(eItemType).Split("-").ToList();
        ids.Add(id.ToString());
        string newIds = string.Join("-", ids);
        switch (eItemType)
        {
            case EItemType.Hair:
                PlayerPrefs.SetString(DataKey.PURCHASED_HAIR, newIds);
                break;
            case EItemType.Pant:
                PlayerPrefs.SetString(DataKey.PURCHASED_PANT, newIds);
                break;
            case EItemType.Shield:
                PlayerPrefs.SetString(DataKey.PURCHASED_SHIELD, newIds);
                break;
            case EItemType.FullSet:
                PlayerPrefs.SetString(DataKey.PURCHASED_FS, newIds);
                break;

        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="eItemType"></param>
    /// <returns>true when success buying</returns>
    public bool PurchaseItem(int id, EItemType eItemType)
    {
        currentBudget = GetCurrentBudget();
        ItemData itemData = GameManager.Ins.ItemDataConfigSO.GetItemData(eItemType, id);
        if (currentBudget >= itemData.Price)
        {
            SavePurchasedItem(eItemType, id);
            ChangeBudget(-itemData.Price);
            return true;
        }
        return false;
    }


    public bool CheckPurchasedItem(int id, EItemType eItemType)
    {
        string[] ids = LoadAllPurchasedItem(eItemType).Split("-");
        if (ids.Contains(id.ToString()))
            return true;
        return false;
    }

    #endregion

    #region EquippedItem

    private string GetKey(EItemType eItemType)
    {
        switch (eItemType)
        {
            case EItemType.Hair:
                return DataKey.EQUIPPED_HAIR;
            case EItemType.Pant:
                return DataKey.EQUIPPED_PANT;
            case EItemType.Shield:
                return DataKey.EQUIPPED_SHIELD;
            case EItemType.FullSet:
                return DataKey.EQUIPPED_FS;
            default:
                return "";
        }
    }

    public bool CheckCurrentEquippedItem(int id, EItemType eItemType)
    {
        string key = GetKey(eItemType);
        int val = PlayerPrefs.GetInt(key);
        if (val == id)
        {
            return true;
        }
        return false;
    }

    public void ChangeSkin(int id, EItemType eItemType)
    {
        player.ChangeSkin(id, eItemType);
    }

    public void EquipItem(int id, EItemType eItemType)
    {
        if (eItemType == EItemType.FullSet)
        {
            UnEquipAll();
        }
        string key = GetKey(eItemType);
        PlayerPrefs.SetInt(key, id);
        ChangeSkin(id, eItemType);
    }


    public void UnEquipItem(EItemType eItemType)
    {
        string key = GetKey(eItemType);
        PlayerPrefs.SetInt(key, 0);
        ChangeSkin(0, eItemType);
    }

    public void UnEquipAll()
    {
        UnEquipItem(EItemType.Hair);
        UnEquipItem(EItemType.Pant);
        UnEquipItem(EItemType.Shield);
        UnEquipItem(EItemType.FullSet);
    }

    public int GetEquippedItem(EItemType eItemType)
    {
        string key = GetKey(eItemType);
        return PlayerPrefs.GetInt(key);
    }

    public void LoadAllEquippedItem()
    {
        if(player==null) return;
        int fsID = GetEquippedItem(EItemType.FullSet);
        if (fsID != 0)
        {
            ChangeSkin(fsID, EItemType.FullSet);
            return;
        }
        ChangeSkin(0, EItemType.FullSet);
        int hairID = GetEquippedItem(EItemType.Hair);
        int pantID = GetEquippedItem(EItemType.Pant);
        int shieldID = GetEquippedItem(EItemType.Shield);
        ChangeSkin(hairID, EItemType.Hair);
        ChangeSkin(pantID, EItemType.Pant);
        ChangeSkin(shieldID, EItemType.Shield);


    }

    public bool CheckEquippedItem(int id, EItemType eItemType)
    {
        if (id == GetEquippedItem(eItemType))
        {
            return true;
        }
        return false;
    }

    #endregion

    #region PurchaseAndEquipWeapon

    public bool PurchaseWeapon(int id)
    {
        currentBudget = GetCurrentBudget();
        WeaponData weaponData = GameManager.Ins.WeaponDataSO.GetWeaponDataById(id);
        if (currentBudget >= weaponData.Price)
        {
            SavePurchasedWeaponData(id);
            ChangeBudget(-weaponData.Price);
            return true;
        }
        return false;
    }

    public void SavePurchasedWeaponData(int id)
    {
        string ids = PlayerPrefs.GetString(DataKey.PURCHASED_WEAPON, "0");
        List<String> listIds = ids.Split('-').ToList();
        listIds.Add(id.ToString());
        ids = string.Join("-", listIds);
        PlayerPrefs.SetString(DataKey.PURCHASED_WEAPON, ids);
    }

    public bool CheckWeaponPurchased(int id)
    {
        Debug.Log(id+","+PlayerPrefs.GetString(DataKey.PURCHASED_WEAPON, "0"));
        string[] ids = PlayerPrefs.GetString(DataKey.PURCHASED_WEAPON, "0").Split('-');
        if (ids.Contains(id.ToString()))
        {
            return true;
        }
        return false;
    }


    /// <summary>
    /// Example: 1_0 is weapon have id 1 and skinId 0
    /// </summary>
    /// <param name="weaponDataId"></param>
    /// <param name="weaponSkinId"></param>
    public void SaveEquippedWeaponData(int weaponDataId,int weaponSkinId)
    {
        StringBuilder val=new StringBuilder(weaponDataId.ToString());
        val.Append("_"+weaponSkinId);
        PlayerPrefs.SetString(DataKey.EQUIPPED_WEAPON, val.ToString());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Tuple (weaponId,SkinId)</returns>
    public Tuple<int,int> GetEquippedWeapon()
    {
        string id=PlayerPrefs.GetString(DataKey.EQUIPPED_WEAPON, "0_0");
        string[] ids=id.Split('_');
        return Tuple.Create(Convert.ToInt32(ids[0]), Convert.ToInt32(ids[1]));
    }

    public bool CheckWeaponEquipped(int weaponId,int weapSkinId){
        var equippedWeapId=GetEquippedWeapon();
        if(equippedWeapId.Item1==weaponId&&equippedWeapId.Item2==weapSkinId){
            return true;
        }
        return false;
    }

    #endregion

    #region WeaponSkin

    public WeaponSkinData GetWeaponSkinDataById(int id, WeaponData weaponData)
    {
        return weaponData.GetWeaponSkinById(id);
    }

    public void InitEquippedWeapon(){
        if(player==null) return;
        var equippedWeapId=GetEquippedWeapon();
        player.InitWeapon(equippedWeapId);
    }

    #endregion

    #region budgetData
    public int GetCurrentBudget()
    {
        currentBudget = PlayerPrefs.GetInt(DataKey.BUDGET);
        return currentBudget;
    }

    private void SetBudget(int budget)
    {
        PlayerPrefs.SetInt(DataKey.BUDGET, budget);
    }

    public void ChangeBudget(int coin)
    {
        int currBudget = GetCurrentBudget();
        currBudget += coin;
        SetBudget(currBudget);
    }

    #endregion

    public void ChangePlayerAnim(string anim){
        if(player==null) return;
        player.ChangeAnim(anim);
    }
}
