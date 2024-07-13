using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetFullScrollView : ScrollView
{
    [SerializeField]private SetFullSkinDataConfigSO setFullSkinDataConfigSO;
    

    protected void GenerateScrollViewItem(List<SetFullSkin> skins){
        for(int i=1;i<skins.Count;i++){
            ScrollViewItem item=Instantiate(scrollViewItem,parentContent);
            item.SetImageIcon(skins[i].icon);
            item.SetID("SF-"+i);
            list.Add(item);
            item.Button.onClick.AddListener(()=>SelectItem(item.ID));
        }
    }
    
    private void Start()
    {
        List<SetFullSkin> setFull=setFullSkinDataConfigSO.SetFullSkins;
        GenerateScrollViewItem(setFull);
    }

    

    
}
