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

    private void Awake(){
        imgSelectGO=ImgSelect.gameObject;
    }

    public void Setup(EItemType eItemType)
    {
        if (UserDataManager.Ins.CheckPurchasedItem(Id,eItemType))
        {
            ImgLock.gameObject.SetActive(false);
        }
        else
        {
            ImgLock.gameObject.SetActive(true);
        }
        
    }

    public void SetImageIcon(Sprite sprite){
        ImgIcon.sprite=sprite;
    }

    public void ChangeSelectedStatus(bool selected){
        imgSelectGO.SetActive(selected);
    }

    public void SetID(int id){
        Id = id;
    }
}
