

using System;
using GloabalEnum;
using UnityEngine;

[Serializable]
public class FullSetImpl : ICharacterSkin
{
    public Transform FullSetHolder;
    private Tuple<EBuffType,float> itemBuff;
    [NonSerialized] public FullSet FullSet;
    public SkinnedMeshRenderer MrPant;
    public SkinnedMeshRenderer MrBody;

    public Tuple<EBuffType,float> GetItemBuff()
    {
        return itemBuff;
    }

    public void SetItemBuff(int id)
    {
        itemBuff=GameManager.Ins.ItemDataConfigSO.GetItemBuff(EItemType.FullSet,id);
    }

    public void InitRandomItem()
    {
        if (FullSet != null)
        {
            FullSet.OnDespawn();
        }
        ItemData fullSetData = GameManager.Ins.ItemDataConfigSO.RandomItemData(EItemType.FullSet);
        FullSet = SimplePool.Spawn<FullSet>(fullSetData.SkinPrefab.FullSet, FullSetHolder);
        FullSet.Setup(MrBody, MrPant);
        SetItemBuff(fullSetData.Id);  
    }

    public void InitSkin(int itemId)
    {
        if (FullSet != null)
        {
            FullSet.OnDespawn();
            FullSet = null;
        }
        ItemData fullSetData = GameManager.Ins.ItemDataConfigSO.GetItemData(EItemType.FullSet, itemId);
        if (itemId != 0)
        {
            FullSet = SimplePool.Spawn<FullSet>(fullSetData.SkinPrefab.FullSet, FullSetHolder);
            FullSet.Setup(MrBody, MrPant);
        }
        else
        {
            MrBody.material = fullSetData.SkinPrefab.FullSet.MrBody.sharedMaterial;
            MrPant.material = fullSetData.SkinPrefab.FullSet.MrPant.sharedMaterial;
        }
        SetItemBuff(itemId);   
    }

    public bool Exists()
    {
        if(FullSet!=null){
            return true;
        }
        return false;
    }
}