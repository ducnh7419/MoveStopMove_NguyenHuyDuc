using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICSkinShopTabView : UICanvas
{
    public Button HairTabBtn;
    public Button PantTabBtn;
    public Button ShieldTabBtn;
    public Button SetFullTabBtn;

    public GameObject HairScrollView; 
    public GameObject PantsScrollView;

    public GameObject ShieldScrollView;
    public GameObject SetFullScrollView;





    private void Start() {
        HairTabBtn.onClick.AddListener(OpenHairShop);
        PantTabBtn.onClick.AddListener(OpenPantShop);
        ShieldTabBtn.onClick.AddListener(OpenShieldShop);
        SetFullTabBtn.onClick.AddListener(OpenFullSetShop);
    }

    private void CloseAll(){
        HairScrollView.SetActive(false);
        PantsScrollView.SetActive(false);
        ShieldScrollView.SetActive(false);
        SetFullScrollView.SetActive(false);
    }

    private void OpenHairShop(){
        CloseAll();
        HairScrollView.SetActive(true);
    }

    private void OpenPantShop(){
        CloseAll();
        PantsScrollView.SetActive(true);
    }

    private void OpenShieldShop(){
        CloseAll();
        ShieldScrollView.SetActive(true);
    }

    private void OpenFullSetShop(){
        CloseAll();
        SetFullScrollView.SetActive(true);
    }
}
