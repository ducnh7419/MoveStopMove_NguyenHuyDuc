
using System;
using System.Collections;
using System.Collections.Generic;
using GloabalEnum;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "ItemDataConfig", menuName = "ScriptableObjects/ItemDataConfig", order = 3)]
public class ItemDataConfigSO : ScriptableObject
{
    public List<ItemData> PantDataConfig = new();
    public List<ItemData> HairDataConfig = new();
    public List<ItemData> ShieldDataConfig = new();
    public List<ItemData> SetFullDataConfig = new();

    public Tuple<EBuffType,float> GetItemBuff(EItemType type, int id)
    {
        if (id==0) return null;
        switch (type)
        {
            case EItemType.Pant:
                for (int i = 0; i < PantDataConfig.Count; i++)
                {
                    if (PantDataConfig[i].Id == id)
                    {
                        return Tuple.Create(PantDataConfig[i].EBuffType,PantDataConfig[i].Value);
                    }
                }
                break;
            case EItemType.Hair:
                for (int i = 0; i < HairDataConfig.Count; i++)
                {
                    if (HairDataConfig[i].Id == id)
                    {
                        return Tuple.Create(HairDataConfig[i].EBuffType,HairDataConfig[i].Value);
                    }
                }
                break;
            case EItemType.Shield:
                for (int i = 0; i < ShieldDataConfig.Count; i++)
                {
                    if (ShieldDataConfig[i].Id == id)
                    {
                        return Tuple.Create(ShieldDataConfig[i].EBuffType,ShieldDataConfig[i].Value);
                    }
                }
                break;
            case EItemType.FullSet:
                for (int i = 0; i < SetFullDataConfig.Count; i++)
                {
                    if (SetFullDataConfig[i].Id == id)
                    {
                        return Tuple.Create(SetFullDataConfig[i].EBuffType,SetFullDataConfig[i].Value);
                    }
                }
                break;
        }
        return null;
    }

    public ItemData GetItemData(EItemType type, int id)
    {
        switch (type)
        {
            case EItemType.Pant:
                for (int i = 0; i < PantDataConfig.Count; i++)
                {
                    if (PantDataConfig[i].Id == id)
                    {
                        return PantDataConfig[i];
                    }
                }
                break;
            case EItemType.Hair:
                for (int i = 0; i < HairDataConfig.Count; i++)
                {
                    if (HairDataConfig[i].Id == id)
                    {
                        return HairDataConfig[i];
                    }
                }
                break;
            case EItemType.Shield:
                for (int i = 0; i < ShieldDataConfig.Count; i++)
                {
                    if (ShieldDataConfig[i].Id == id)
                    {
                        return ShieldDataConfig[i];
                    }
                }
                break;
            case EItemType.FullSet:
                for (int i = 0; i < SetFullDataConfig.Count; i++)
                {
                    if (SetFullDataConfig[i].Id == id)
                    {
                        return SetFullDataConfig[i];
                    }
                }
                break;
        }
        return null;
    }

    public ItemData RandomItemData(EItemType type)
    {
        List<ItemData> list;
        int rdn;
        switch (type)
        {
            case EItemType.None:
                return null;
            case EItemType.Pant:
                list = PantDataConfig;
                rdn=Random.Range(1, list.Count);
                return list[rdn];
            case EItemType.Hair:
                list = HairDataConfig;
                rdn=Random.Range(1, list.Count);
                return list[rdn];
            case EItemType.Shield:
                list = ShieldDataConfig;
                rdn=Random.Range(1, list.Count);
                return list[rdn];      
            case EItemType.FullSet:
                list = SetFullDataConfig;
                rdn=Random.Range(1, list.Count);
                return list[rdn];
        }
        return null;
        
    }
}



    [System.Serializable]
    public class ItemData
    {
        public int Id;
        public string Name;
        public string Description;
        public Sprite icon;
        public EItemType ItemType;
        public EBuffType EBuffType;
        public float Value;
        public Skin SkinPrefab;
        public int Price;
    }
