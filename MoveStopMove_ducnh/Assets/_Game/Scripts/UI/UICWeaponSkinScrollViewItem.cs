using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICWeaponSkinScrollViewItem : MonoBehaviour
{
    public int Id;
    public Image imgSelect;
    private GameObject imgSelectGO;
    public Image imgItem;
    public Button buttonGO;

    private void Start() {
        imgSelectGO=imgSelect.gameObject;
    }

    public void Setup(int id,WeaponSkinData weaponSkinData){
        buttonGO.onClick.AddListener(OnClick);
        imgItem.sprite=weaponSkinData.Icon;
        Id=id;
    }

    public void OnClick(){
        
    }

    public void ChangeSelectState(bool state){
        imgSelectGO.SetActive(state);
    }

}
