using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GloabalEnum;
using UnityEngine;

public class PantsScrollView : ScrollView
{

    private void Awake() {
        eItemType=EItemType.Pant;
    }

    protected override void Start()
    {
        base.Start();
        List<ItemData> items = GameManager.Ins.ItemDataConfigSO.PantDataConfig;
        GenerateScrollViewItem(items);
    }
}
