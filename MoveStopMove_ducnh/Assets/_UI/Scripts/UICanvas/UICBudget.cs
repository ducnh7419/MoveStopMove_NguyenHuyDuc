using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICBudget : UICanvas
{
    public TextMeshProUGUI textBudget;
    // Update is called once per frame
    void Update()
    {
        int currentBudget=UserDataManager.Ins.GetCurrentBudget();
        textBudget.text=currentBudget.ToString();
    }
}
