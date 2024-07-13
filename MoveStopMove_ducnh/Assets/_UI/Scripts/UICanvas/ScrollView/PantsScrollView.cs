using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PantsScrollView : ScrollView
{
    [SerializeField]private PantDataConfig pantDataConfig;
    
    protected void GenerateScrollViewItem(List<PantSkin> skins){
        for(int i=1;i<skins.Count;i++){
            ScrollViewItem item=Instantiate(scrollViewItem,parentContent);
            item.SetImageIcon(skins[i].icon);
            item.SetID(id);
            id++;
            list.Add(item);
            item.Button.onClick.AddListener(()=>SelectItem(item.ID));
        }
    }

    private void Start()
    {
        List<PantSkin> pantSkins=pantDataConfig.PantSkin;
        GenerateScrollViewItem(pantSkins);
    }

    
}
