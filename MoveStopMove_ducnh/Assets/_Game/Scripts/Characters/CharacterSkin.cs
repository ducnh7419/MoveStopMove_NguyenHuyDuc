using System;
using System.Collections;
using System.Collections.Generic;
using GloabalEnum;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class CharacterSkin
{
    [NonSerialized] public Skin HairSkin;

    [NonSerialized] public Material PantSkin;

    [NonSerialized] public Skin ShieldSkin;


    public Transform WingHolder;
    public Transform HairHolder;
    public Transform TailHolder;
    public Transform ShieldHolder;
    public SkinnedMeshRenderer MrPant;


    public void InitRandomItem(){
        ItemData hairData=GameManager.Ins.ItemDataConfigSO.RandomItemData(GloabalEnum.EItemType.Hair);
        ItemData pantData=GameManager.Ins.ItemDataConfigSO.RandomItemData(GloabalEnum.EItemType.Pant);
        // ItemData shieldData=GameManager.Ins.ItemDataConfigSO.RandomItemData(GloabalEnum.EItemType.Shield);
        HairSkin=SimplePool.Spawn<Skin>(hairData.SkinPrefab,HairHolder);
        PantSkin=pantData.SkinPrefab.MrPant.sharedMaterial;
        // characterSkin.ShieldSkin=SimplePool.Spawn<Skin>(shieldData.SkinPrefab);
        
    }

    public void InitRandomFullSet(){
        ItemData fullSet=GameManager.Ins.ItemDataConfigSO.RandomItemData(EItemType.FullSet);
        SimplePool.Spawn(fullSet.SkinPrefab.HairHolder);
    }

    public void InitFullSetSkin(int id){
        ItemData fullSet=GameManager.Ins.ItemDataConfigSO.GetItemData(EItemType.FullSet,id);
        SimplePool.Spawn<SkinHolder>(fullSet.SkinPrefab.HairHolder,fullSet.SkinPrefab.HairHolder.TF);
        SimplePool.Spawn<SkinHolder>(fullSet.SkinPrefab.WingHolder,fullSet.SkinPrefab.HairHolder.TF);
        // SimplePool.Spawn(fullSet.SkinPrefab.TailHolder);
    }

    public void InitSkin(EItemType eItemType,int itemId){
        ItemData data=GameManager.Ins.ItemDataConfigSO.GetItemData(eItemType,itemId);
        switch(eItemType){
            case EItemType.Hair:
                HairSkin=SimplePool.Spawn<Skin>(data.SkinPrefab,HairHolder);
                break;
            case EItemType.Pant:
                PantSkin=data.SkinPrefab.MrPant.sharedMaterial;
                MrPant.material=PantSkin;
                break;
            case EItemType.Shield:
                ShieldSkin=SimplePool.Spawn<Skin>(data.SkinPrefab,ShieldHolder);
                break;
        }
    }


}
