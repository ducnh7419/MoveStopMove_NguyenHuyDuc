using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GloabalEnum;
using UnityEngine;

public class FullSetScrollView : ScrollView
{   


    protected override void Start()
    {
        base.Start();
        List<ItemData> items=GameManager.Ins.ItemDataConfigSO.SetFullDataConfig;
        GenerateScrollViewItem(items);
    }

    

    
}
