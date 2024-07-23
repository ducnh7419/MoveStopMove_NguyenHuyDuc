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

    [NonSerialized] public FullSet FullSet;

    public Transform FullSetHolder;
    public Transform WingHolder;
    public Transform HairHolder;
    public Transform ShieldHolder;
    public Transform FullSkinRoot;
    public SkinnedMeshRenderer MrPant;
    public SkinnedMeshRenderer MrBody;


    public void InitRandomItem(){
        ItemData hairData=GameManager.Ins.ItemDataConfigSO.RandomItemData(GloabalEnum.EItemType.Hair);
        ItemData pantData=GameManager.Ins.ItemDataConfigSO.RandomItemData(GloabalEnum.EItemType.Pant);
        // ItemData shieldData=GameManager.Ins.ItemDataConfigSO.RandomItemData(GloabalEnum.EItemType.Shield);
        HairSkin=SimplePool.Spawn<Skin>(hairData.SkinPrefab,HairHolder);
        PantSkin=pantData.SkinPrefab.MrPant.sharedMaterial;
        // characterSkin.ShieldSkin=SimplePool.Spawn<Skin>(shieldData.SkinPrefab);
        
    }

    public void InitRandomFullSet(){
        if(FullSet!=null){
            FullSet.OnDespawn();
        }
        ItemData fullSet=GameManager.Ins.ItemDataConfigSO.RandomItemData(EItemType.FullSet);
        FullSet=SimplePool.Spawn<FullSet>(fullSet.SkinPrefab.FullSet,FullSetHolder);
        FullSet.Setup(MrBody,MrPant);
    }

    public void InitFullSetSkin(int id){
        if(FullSet!=null){
            FullSet.OnDespawn();
        }
        ItemData fullSet=GameManager.Ins.ItemDataConfigSO.GetItemData(EItemType.FullSet,id);
        FullSet=SimplePool.Spawn<FullSet>(fullSet.SkinPrefab.FullSet,FullSetHolder);
        FullSet.Setup(MrBody,MrPant);
    }

    public void InitSkin(EItemType eItemType,int itemId){
        ItemData data=GameManager.Ins.ItemDataConfigSO.GetItemData(eItemType,itemId);
        switch(eItemType){
            case EItemType.Hair:
                if(HairSkin!=null){
                    HairSkin.OnDespawn();
                }
                HairSkin=SimplePool.Spawn<Skin>(data.SkinPrefab,HairHolder);
                HairSkin.SetScale(data.SkinPrefab.TF.localScale);
                break;
            case EItemType.Pant:
                PantSkin=data.SkinPrefab.MrPant.sharedMaterial;
                MrPant.material=PantSkin;
                break;
            case EItemType.Shield:
                if(ShieldSkin!=null){
                    ShieldSkin.OnDespawn();
                }
                ShieldSkin=SimplePool.Spawn<Skin>(data.SkinPrefab,ShieldHolder);
                break;
        }
    }


}
