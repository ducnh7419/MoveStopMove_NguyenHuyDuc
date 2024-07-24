using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GloabalEnum;
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

    public string LoadAllPurchasedItem(EItemType eItemType)
    {
        switch (eItemType)
        {
            case EItemType.Hair:
                return PlayerPrefs.GetString("Purchased Hair");
            case EItemType.Pant:
                return PlayerPrefs.GetString("Purchased Pant");
            case EItemType.Shield:
                return PlayerPrefs.GetString("Purchased Shield");
            case EItemType.FullSet:
                return PlayerPrefs.GetString("Purchased Full Set");

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
                PlayerPrefs.SetString("Purchased Hair", newIds);
                break;
            case EItemType.Pant:
                PlayerPrefs.SetString("Purchased Pant", newIds);
                break;
            case EItemType.Shield:
                PlayerPrefs.SetString("Purchased Shield", newIds);
                break;
            case EItemType.FullSet:
                PlayerPrefs.SetString("Purchased Full Set", newIds);
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
            DecreseBudget(itemData.Price);
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

    private string GetKey(EItemType eItemType)
    {
        string key = "";
        switch (eItemType)
        {
            case EItemType.Hair:
                key = "Hair";
                return key;
            case EItemType.Pant:
                key = "Pant";
                return key;
            case EItemType.Shield:
                key = "Shield";
                return key;
            case EItemType.FullSet:
                key = "Full Set";
                return key;
            default:
                key = "";
                return key;
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
        ChangeSkin(0,eItemType);
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
        int fsID = GetEquippedItem(EItemType.FullSet);
        if(fsID!=0){
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


    // public bool PurchaseWeapon(int id){
    //     currentBudget=GetCurrentBudget();
    //     if(currentBudget>=itemData.Price){
    //         PlayerPrefs.SetInt("WP-"+id,1);
    //         DecreseBudget(itemData.Price);
    //         return true;
    //     }
    //     return false;
    // }

    public int GetCurrentBudget()
    {
        currentBudget = PlayerPrefs.GetInt("Budget");
        return currentBudget;
    }

    private void SetBudget(int budget)
    {
        PlayerPrefs.SetInt("Budget", budget);
    }

    public void AddBudget(int coin)
    {
        int currBudget = GetCurrentBudget();
        currBudget += coin;
        SetBudget(currBudget);
    }

    public void DecreseBudget(int coin)
    {
        int currBudget = GetCurrentBudget();
        currBudget -= coin;
        SetBudget(currBudget);
    }
}
