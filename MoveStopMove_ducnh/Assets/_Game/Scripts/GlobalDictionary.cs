using System.Collections;
using System.Collections.Generic;
using GloabalEnum;
using UnityEngine;


    public static class GlobalDictionary
    {
        public static Dictionary<EItemType, string> IdPrefix = new()
        {
            [EItemType.Pant]="P",
            [EItemType.Hair]="H",
            [EItemType.Shield]="S",
            [EItemType.FullSet]="FS"
        }; 
    }

    



