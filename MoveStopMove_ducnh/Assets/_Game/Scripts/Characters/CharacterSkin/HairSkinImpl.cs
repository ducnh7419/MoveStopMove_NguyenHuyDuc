

using System;
using GloabalEnum;
using UnityEngine;

[Serializable]
public class HairSkinImpl : ICharacterSkin
{
    private Tuple<EBuffType,float> itemBuff;
    public Transform HairHolder;
    [NonSerialized] public Skin HairSkin;

    public void SetItemBuff(int id)
    {
        if(HairSkin==null) return;
        itemBuff=GameManager.Ins.ItemDataConfigSO.GetItemBuff(EItemType.Hair,id);
    }

    public void InitRandomItem()
    {
        if(HairSkin!=null)
            HairSkin.OnDespawn(); 
        ItemData hairData = GameManager.Ins.ItemDataConfigSO.RandomItemData(EItemType.Hair);
        HairSkin = SimplePool.Spawn<Skin>(hairData.SkinPrefab, HairHolder);
        SetItemBuff(hairData.Id);
    }

    public void InitSkin(int itemId)
    {
        ItemData data = GameManager.Ins.ItemDataConfigSO.GetItemData(EItemType.Hair, itemId);
        if (HairSkin != null)
        {
            HairSkin.OnDespawn();
            HairSkin = null;
        }
        if (itemId == 0) return;
        HairSkin = SimplePool.Spawn<Skin>(data.SkinPrefab, HairHolder);
        HairSkin.SetScale(data.SkinPrefab.TF.localScale);
        SetItemBuff(data.Id);
    }

    public Tuple<EBuffType,float> GetItemBuff()
    {
        return itemBuff;
    }

    public bool Exists()
    {
        if(HairSkin!=null){
            return true;
        }
        return false;
    }

    public void TakeOffSkin()
    {
        throw new NotImplementedException();
    }
}