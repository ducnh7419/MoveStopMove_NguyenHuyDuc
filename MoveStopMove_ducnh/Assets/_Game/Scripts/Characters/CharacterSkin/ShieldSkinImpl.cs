

using System;
using GloabalEnum;
using UnityEngine;

[Serializable]
public class ShieldSkinImpl : ICharacterSkin
{
    private Tuple<EBuffType,float> itemBuff;
    public Transform ShieldHolder;
    [NonSerialized] public Skin ShieldSKin;

    public void SetItemBuff(int id)
    {
        if(ShieldSKin==null) return;
        itemBuff=GameManager.Ins.ItemDataConfigSO.GetItemBuff(EItemType.Shield,id);
    }

    public void InitRandomItem()
    {
        if(ShieldSKin!=null)
            ShieldSKin.OnDespawn(); 
        ItemData shieldData = GameManager.Ins.ItemDataConfigSO.RandomItemData(EItemType.Shield);
        ShieldSKin = SimplePool.Spawn<Skin>(shieldData.SkinPrefab, ShieldHolder);
        SetItemBuff(shieldData.Id);
    }

    public void InitSkin(int itemId)
    {
        ItemData data = GameManager.Ins.ItemDataConfigSO.GetItemData(EItemType.Shield, itemId);
        if (ShieldSKin != null)
        {
            ShieldSKin.OnDespawn();
            ShieldSKin = null;
        }
        if (itemId == 0) return;
        ShieldSKin = SimplePool.Spawn<Skin>(data.SkinPrefab, ShieldHolder);
        ShieldSKin.SetScale(data.SkinPrefab.TF.localScale);
        SetItemBuff(data.Id);
    }

    public Tuple<EBuffType,float> GetItemBuff()
    {
        return itemBuff;
    }

    public bool Exists()
    {
        if(ShieldSKin!=null){
            return true;
        }
        return false;
    }

    public void TakeOffSkin()
    {
        throw new NotImplementedException();
    }
}