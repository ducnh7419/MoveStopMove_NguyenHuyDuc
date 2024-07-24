using System;
using System.Collections;
using System.Collections.Generic;
using GloabalEnum;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class ScrollView : UICanvas
{
    protected int id;
    private GameObject btnPurchaseGO;
    private bool isActive;
    private GameObject btnSelectGO;
    private GameObject btnUnEquipGO;
    protected List<ScrollViewItem> list = new();
    private ScrollViewItem selectedItem;
    public Transform parentContent;
    [SerializeField] protected ScrollViewItem scrollViewItem;
    [SerializeField] private Button btnPurchase;
    [SerializeField] private Button btnSelect;
    [SerializeField] private Button btnUnEquip;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI priceText;

    private void OnEnable() {
        isActive=true;
        for(int i=0; i<list.Count;i++){
            if(list[i].IsEquipped){
                SelectItem(list[i].Id);
                return;
            }
        }
        if(list.Count==0) return;
        SelectItem(list[0].Id);
        Debug.Log(selectedItem.Id);
        
    }

    private void OnDisable() {
        isActive=false;
    }
    
    protected virtual void Start()
    {
        btnPurchaseGO = btnPurchase.gameObject;
        btnSelectGO = btnSelect.gameObject;
        btnUnEquipGO = btnUnEquip.gameObject;
        btnPurchase.onClick.AddListener(OnPurchaseBtnClicked);
        btnSelect.onClick.AddListener(OnSelectBtnClicked);
        btnUnEquip.onClick.AddListener(OnUnEquipBtnClicked);
    }

    private void OnPurchaseBtnClicked()
    {
        if(isActive==false) return;
        var item = GameManager.Ins.ItemDataConfigSO.GetItemData(selectedItem.EItemType, selectedItem.Id);
        if (UserDataManager.Ins.PurchaseItem(item.Id, selectedItem.EItemType))
        {
            ChangeButtonStatus(EItemState.Purchased);
            selectedItem.SetItemState(EItemState.Purchased);
        }
    }

    public void DeActiveAll()
    {
        btnPurchaseGO.SetActive(false);
        btnSelectGO.SetActive(false);
        btnUnEquipGO.SetActive(false);
    }

    private void OnSelectBtnClicked()
    {
        if(isActive==false) return;
        var item = GameManager.Ins.ItemDataConfigSO.GetItemData(selectedItem.EItemType, selectedItem.Id);
        UserDataManager.Ins.EquipItem(item.Id, selectedItem.EItemType);
        for(int i=0;i<list.Count;i++){
            if(list[i].Id!=selectedItem.Id){
                if(list[i].EItemState==EItemState.Equipped){
                    list[i].SetItemState(EItemState.NotEquipped);
                    break;
                }
            }
        }
        ChangeButtonStatus(EItemState.Equipped);
        selectedItem.SetItemState(EItemState.Equipped);
    }

    private void OnUnEquipBtnClicked()
    {
        if(isActive==false) return;
        UserDataManager.Ins.UnEquipItem(selectedItem.EItemType);
        ChangeButtonStatus(EItemState.NotEquipped);
        selectedItem.SetItemState(EItemState.NotEquipped);
    }

    protected void GenerateScrollViewItem(List<ItemData> items)
    {
        for (int i = 1; i < items.Count; i++)
        {
            ScrollViewItem item = Instantiate(scrollViewItem, parentContent);
            item.SetImageIcon(items[i].icon);
            item.SetID(items[i].Id);
            item.SetItemType(items[i].ItemType);
            item.Button.onClick.AddListener(() => SelectItem(item.Id));
            item.Setup(item.EItemType);
            list.Add(item);
            if(item.IsEquipped){
                SelectItem(item.Id);
            }
        }
    }

    public void ChangeButtonStatus(EItemState eItemStatus)
    {
        switch (eItemStatus){
            case EItemState.NotPurchased:
                DeActiveAll();
                btnPurchaseGO.SetActive(true);
                break;
            case EItemState.Purchased:
                DeActiveAll();
                btnSelectGO.SetActive(true);
                break;
            case EItemState.Equipped:
                DeActiveAll();
                btnUnEquipGO.SetActive(true);
                break;
            case EItemState.NotEquipped:
                DeActiveAll();
                btnSelectGO.SetActive(true);
                break;
        }
    }


    public void SelectItem(int id)
    {
        Debug.Log("Clicked");
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Id == id)
            {
                list[i].ChangeSelectedStatus(true);
                ChangeButtonStatus(list[i].EItemState);
                Debug.Log(list[i].EItemState);
                selectedItem=list[i];
            }
            else
            {
                list[i].ChangeSelectedStatus(false);
            }
        }
        var item = GameManager.Ins.ItemDataConfigSO.GetItemData(selectedItem.EItemType, id);
        if (item.Price > UserDataManager.Ins.GetCurrentBudget())
        {
            priceText.color = Color.red;
        }else{
            priceText.color = Color.black;
        }
        description.text = item.Description;
        priceText.text = item.Price.ToString();
        UserDataManager.Ins.ChangeSkin(item.Id, selectedItem.EItemType);
    }
}
