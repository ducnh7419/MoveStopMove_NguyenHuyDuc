using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GloabalEnum;
using UnityEngine;

public class ShieldScrollView : ScrollView
{

    
    protected override void Start()
    {
        base.Start();
        List<ItemData> items=GameManager.Ins.ItemDataConfigSO.ShieldDataConfig;
        GenerateScrollViewItem(items);
    }

    

    
}
