

using System;
using GloabalEnum;
using UnityEngine;

[Serializable]
public class PantSkinImpl : ICharacterSkin
{
    private Tuple<EBuffType,float> itemBuff;
    [NonSerialized] public Material PantSkin;
    public SkinnedMeshRenderer MrPant;


    public void SetItemBuff(int id)
    {
        itemBuff=GameManager.Ins.ItemDataConfigSO.GetItemBuff(EItemType.Pant,id);
    }

    public void InitRandomItem()
    {
        ItemData pantData = GameManager.Ins.ItemDataConfigSO.RandomItemData(EItemType.Pant);
        PantSkin = pantData.SkinPrefab.MrPant.sharedMaterial;
        SetItemBuff(pantData.Id);
    }

    public void InitSkin(int itemId)
    {
        ItemData pantData = GameManager.Ins.ItemDataConfigSO.GetItemData(EItemType.Pant, itemId);
        PantSkin = pantData.SkinPrefab.MrPant.sharedMaterial;
        MrPant.material = PantSkin;
        if (itemId == 0)
        {
            PantSkin = null;
        }
        SetItemBuff(itemId);
    }

    public Tuple<EBuffType, float> GetItemBuff()
    {
        return itemBuff;
    }

    public bool Exists()
    {
        if(PantSkin!=null){
            return true;
        }
        return false;
    }

    public void TakeOffSkin()
    {
        throw new NotImplementedException();
    }
}