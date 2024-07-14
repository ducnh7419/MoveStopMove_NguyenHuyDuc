using System;
using System.Collections;
using System.Collections.Generic;
using GloabalEnum;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollView : UICanvas
{
    protected int id;
    private GameObject btnPurchaseGO;
    private GameObject btnSelectGO;
    private GameObject btnUnEquipGO;
    protected EItemType eItemType;
    protected List<ScrollViewItem> list=new();
    private ItemData selectedItem;
    public Transform parentContent;
    [SerializeField]protected ScrollViewItem scrollViewItem;
    [SerializeField] private Button btnPurchase;
    [SerializeField] private Button btnSelect;
    [SerializeField] private Button btnUnEquip;
    [SerializeField] TextMeshPro description;

    protected virtual void Start(){
        btnPurchaseGO=btnPurchase.gameObject;
        btnSelectGO=btnSelect.gameObject;
        btnUnEquipGO=btnUnEquip.gameObject;
        btnPurchase.onClick.AddListener(OnPurchaseBtnClicked);
        btnSelect.onClick.AddListener(OnSelectBtnClicked);
        btnUnEquip.onClick.AddListener(OnUnEquipBtnClicked);
    }

    private void OnPurchaseBtnClicked()
    {
        if(UserDataManager.Ins.PurchaseItem(selectedItem.Id,eItemType)){
            btnPurchaseGO.SetActive(false);
            btnSelectGO.SetActive(true);
        }
    }

    private void OnSelectBtnClicked(){
        UserDataManager.Ins.EquipItem(selectedItem.Id,eItemType);
        btnSelectGO.SetActive(false);
        btnUnEquipGO.SetActive(true);
    }

    private void OnUnEquipBtnClicked(){
        UserDataManager.Ins.UnEquipItem(eItemType);
        btnSelectGO.SetActive(true);
        btnUnEquipGO.SetActive(false);
    }

    protected void GenerateScrollViewItem(List<ItemData> items){
        for(int i=0;i<items.Count;i++){
            ScrollViewItem item=Instantiate(scrollViewItem,parentContent);
            item.SetImageIcon(items[i].icon);
            item.SetID(items[i].Id);
            list.Add(item);
            item.Button.onClick.AddListener(()=>SelectItem(item.ID));
            item.Setup(eItemType);
        }
    }
   

   public void SelectItem(int id){
        Debug.Log("Clicked");
        for (int i=0;i<list.Count;i++){
            if(list[i].ID==id){
                list[i].ChangeSelectedStatus(true);
                selectedItem=GameManager.Ins.ItemDataConfigSO.GetItemData(eItemType,list[i].ID);
                description.text=selectedItem.Description;
                if(!UserDataManager.Ins.CheckPurchasedItem(selectedItem.Id,eItemType)) return;
                btnPurchaseGO.SetActive(false);
                if(UserDataManager.Ins.CheckCurrentEquippedItem(selectedItem.Id,eItemType)){
                    btnSelectGO.SetActive(false);
                    btnUnEquipGO.SetActive(true);
                }else{
                    btnUnEquipGO.SetActive(false);
                    btnSelectGO.SetActive(true);
                }
            }else{
                list[i].ChangeSelectedStatus(false);
            }
        }
    }
}
