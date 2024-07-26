using System;
using System.Collections;
using System.Collections.Generic;
using GloabalEnum;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICShopWeapon : UICanvas
{
    private GameObject btnPurchaseGO;
    private int currentidx;
    private List<WeaponData> weaponDatas;
    private GameObject btnSelectGO;
    private GameObject btnEquippedGO;
    private EItemState currItemState;
    [SerializeField] private Button btnClose;
     [SerializeField] private Button btnPurchase;
    [SerializeField] private Button btnSelect;
    [SerializeField] private Button btnEquipped;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;
    [SerializeField] private Image image;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject lockTextGO;
    [SerializeField] private Transform skinTabParentTF;
    [SerializeField] private UICWeaponSkinScrollView uICWeaponSkinScrollViewPrefab;
    private List<GameObject> scrollViewGOList=new();
    private List<UICWeaponSkinScrollView> scrollViewList=new();
    private GameObject currScrollViewGO;
    private UICWeaponSkinScrollView currScrollView;


    protected virtual void Start()
    {
        btnPurchaseGO = btnPurchase.gameObject;
        btnSelectGO = btnSelect.gameObject;
        btnEquippedGO = btnEquipped.gameObject;
        btnClose.onClick.AddListener(OnClose);
        btnPurchase.onClick.AddListener(OnPurchaseBtnClicked);
        btnSelect.onClick.AddListener(OnSelectBtnClicked);
        btnEquipped.onClick.AddListener(OnEquippedBtnClicked);
        nextButton.onClick.AddListener(()=>InitItemUI(++currentidx));
        prevButton.onClick.AddListener(()=>InitItemUI(--currentidx));
        weaponDatas=GameManager.Ins.WeaponDataSO.Weapons;
        for(int i=0;i<weaponDatas.Count;i++){
            UICWeaponSkinScrollView uICWeaponSkinScrollView=Instantiate(uICWeaponSkinScrollViewPrefab,skinTabParentTF);
            uICWeaponSkinScrollView.Setup(weaponDatas[i].Id,weaponDatas[i],this);
            uICWeaponSkinScrollView.SetAction(ChangeUIState);
            GameObject GO=uICWeaponSkinScrollView.gameObject;
            scrollViewGOList.Add(GO);
            scrollViewList.Add(uICWeaponSkinScrollView);
            SetActiveSkinTab(GO,false);
        }
        InitItemUI(currentidx);
        SetActiveSkinTab(scrollViewGOList[currentidx],true);
    }

    private void OnClose()
    {
        GameManager.Ins.ChangeState(GameManager.State.MainMenu);
        UserDataManager.Ins.InitEquippedWeapon(); 
    }

    public void SetActiveSkinTab(GameObject skinTab,bool active){
        if(currScrollViewGO!=null)
            currScrollViewGO.SetActive(false);
        currScrollViewGO=skinTab;
        skinTab.SetActive(active);
    }

    public void TestUA(int index){
        Debug.Log(index);
    }

    public void SetCurrentScrollView(UICWeaponSkinScrollView uICWeaponSkinScrollView){
        currScrollView=uICWeaponSkinScrollView;
    }

    public void SetWeaponImage(Sprite sprite){
        image.sprite=sprite;
    }

    private void InitItemUI(int index){
        Debug.Log("Push");
        int newIndex=Math.Abs(index)%weaponDatas.Count;
        nameText.text=weaponDatas[newIndex].Name;
        priceText.text=weaponDatas[newIndex].Price.ToString();
        image.sprite=weaponDatas[newIndex].Icon;
        description.text=weaponDatas[newIndex].Description;
        SetCurrentScrollView(scrollViewList[newIndex]);
        ChangeUIState();
    }

    

    private void CheckCurrentItemState(){
        int newIndex=Math.Abs(currentidx)%weaponDatas.Count;
        if(UserDataManager.Ins.CheckWeaponPurchased(newIndex)){
            if(UserDataManager.Ins.CheckWeaponEquipped(newIndex,currScrollView.SelectedId)){
                currItemState=EItemState.Equipped;
            }else{
                currItemState=EItemState.NotEquipped;
            }
        }else{
            currItemState=EItemState.NotPurchased;
        }
    }

    private void GetCurrenEquippedWeapon(){
        
    }

    private void OnEquippedBtnClicked()
    {
        
    }

    private void OnSelectBtnClicked()
    {
        int newIndex=Math.Abs(currentidx)%weaponDatas.Count;
        currItemState=EItemState.Equipped;
        ChangeUIState(currItemState);
        UserDataManager.Ins.SaveEquippedWeaponData(newIndex,currScrollView.SelectedId);
    }

    private void OnPurchaseBtnClicked()
    {
        int newIndex=Math.Abs(currentidx)%weaponDatas.Count;
        UserDataManager.Ins.PurchaseWeapon(weaponDatas[newIndex].Id);
        currItemState=EItemState.NotEquipped;
        ChangeUIState(currItemState);
        SetActiveSkinTab(scrollViewGOList[newIndex],true);
    }

    public void ChangeUIState(){
        CheckCurrentItemState();
        ChangeUIState(currItemState);
    }

    public void ChangeUIState(EItemState eItemState)
    {
        int newIndex=Math.Abs(currentidx)%weaponDatas.Count;
        switch (eItemState){
            case EItemState.NotPurchased:
                DeActiveAll();
                btnPurchaseGO.SetActive(true);
                lockTextGO.SetActive(true);
                SetActiveSkinTab(scrollViewGOList[newIndex],false);
                break;
            case EItemState.Equipped:
                DeActiveAll();
                btnEquippedGO.SetActive(true);
                lockTextGO.SetActive(false);
                SetActiveSkinTab(scrollViewGOList[newIndex],true);
                break;
            case EItemState.NotEquipped:
                DeActiveAll();
                btnSelectGO.SetActive(true);
                lockTextGO.SetActive(false);
                SetActiveSkinTab(scrollViewGOList[newIndex],true);
                break;
        }
    }
    public void DeActiveAll()
    {
        btnPurchaseGO.SetActive(false);
        btnSelectGO.SetActive(false);
        btnEquippedGO.SetActive(false);
    }
}
