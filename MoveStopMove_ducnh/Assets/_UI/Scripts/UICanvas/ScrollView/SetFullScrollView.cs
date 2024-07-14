using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GloabalEnum;
using UnityEngine;

public class SetFullScrollView : ScrollView
{   

    private void Awake() {
        eItemType=EItemType.FullSet;
    }

    protected override void Start()
    {
        base.Start();
        List<ItemData> items=GameManager.Ins.ItemDataConfigSO.SetFullDataConfig;
        GenerateScrollViewItem(items);
    }

    

    
}
