using System.Collections;
using System.Collections.Generic;
using GloabalEnum;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewItem : MonoBehaviour
{
    public int Id;
    public Button Button;
    public Image ImgSelect;
    public Image ImgIcon;
    public Image ImgLock;
    private GameObject imgSelectGO;
    public EItemState EItemState;
    public EItemType EItemType;
    public bool IsEquipped;

    private void Awake()
    {
        imgSelectGO = ImgSelect.gameObject;
    }

    public void Setup(EItemType eItemType)
    {
        if (UserDataManager.Ins.CheckPurchasedItem(Id, eItemType))
        {
            ImgLock.gameObject.SetActive(false);
            if (UserDataManager.Ins.CheckEquippedItem(Id, eItemType))
            {
                ChangeSelectedStatus(true);
                SetItemState(EItemState.Equipped);
                IsEquipped=true;
            }
            else
            {
                ChangeSelectedStatus(false);
                SetItemState(EItemState.NotEquipped);
                IsEquipped=false;
            }
        }
        else
        {
            ImgLock.gameObject.SetActive(true);
            SetItemState(EItemState.NotPurchased);
        }
    }

    public void SetImageIcon(Sprite sprite)
    {
        ImgIcon.sprite = sprite;
    }

    public void ChangeSelectedStatus(bool selected)
    {
        imgSelectGO.SetActive(selected);
    }

    public void SetID(int id)
    {
        Id = id;
    }

    public void SetItemType(EItemType eItemType){
        EItemType = eItemType;
    }

    public void SetItemState(EItemState eItemState){
        this.EItemState=eItemState;
    }
}
