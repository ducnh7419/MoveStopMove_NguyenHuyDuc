using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GloabalEnum;
using UnityEngine;

public class PantsScrollView : ScrollView
{


    protected override void Start()
    {
        base.Start();
        List<ItemData> items = GameManager.Ins.ItemDataConfigSO.PantDataConfig;
        GenerateScrollViewItem(items);
    }
}
