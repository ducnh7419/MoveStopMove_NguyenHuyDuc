using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShieldScrollView : ScrollView
{
    [SerializeField]private ShieldDataConfigSO shieldDataConfigSO;
    

    protected void GenerateScrollViewItem(List<ShieldSkin> skins){
        for(int i=1;i<skins.Count;i++){
            ScrollViewItem item=Instantiate(scrollViewItem,parentContent);
            item.SetImageIcon(skins[i].icon);
            item.SetID("S"+i);
            list.Add(item);
            item.Button.onClick.AddListener(()=>SelectItem(item.ID));
        }
    }
    
    private void Start()
    {
        List<ShieldSkin> shieldSkins=shieldDataConfigSO.ShieldSkin;
        GenerateScrollViewItem(shieldSkins);
    }

    

    
}
