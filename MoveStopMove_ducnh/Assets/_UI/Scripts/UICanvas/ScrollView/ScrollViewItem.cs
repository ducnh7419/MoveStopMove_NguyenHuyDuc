using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewItem : MonoBehaviour
{
    public string ID;
    public Button Button;
    public Image ImgSelect;
    public Image ImgIcon;
    public Image ImgLock;

    private void Start(){
        
    }

    public void Setup()
    {
        if (UserDataManager.Ins.CheckPurchasedItem(ID))
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
        ImgSelect.gameObject.SetActive(selected);
    }

    public void SetID(string id){
        ID = id;
    }
}
