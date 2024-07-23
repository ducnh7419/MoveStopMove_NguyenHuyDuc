using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICShopWeapon : MonoBehaviour
{
    private GameObject btnPurchaseGO;
    private GameObject btnSelectGO;
    private GameObject btnEquippedGO;
     [SerializeField] private Button btnPurchase;
    [SerializeField] private Button btnSelect;
    [SerializeField] private Button btnEquipped;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI priceText;

    protected virtual void Start()
    {
        btnPurchaseGO = btnPurchase.gameObject;
        btnSelectGO = btnSelect.gameObject;
        btnEquippedGO = btnEquippedGO.gameObject;
        btnPurchase.onClick.AddListener(OnPurchaseBtnClicked);
        btnSelect.onClick.AddListener(OnSelectBtnClicked);
        btnEquipped.onClick.AddListener(OnEquippedBtnClicked);
    }

    private void GetCurrenEquippedWeapon(){
        
    }

    private void OnEquippedBtnClicked()
    {
        throw new NotImplementedException();
    }

    private void OnSelectBtnClicked()
    {
        throw new NotImplementedException();
    }

    private void OnPurchaseBtnClicked()
    {
        throw new NotImplementedException();
    }
}
