using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HairScrollView : ScrollView
{
    [SerializeField]private HairDataConfigSO hairDataConfigSO;
    

    protected void GenerateScrollViewItem(List<HairSkin> skins){
        for(int i=1;i<skins.Count;i++){
            ScrollViewItem item=Instantiate(scrollViewItem,parentContent);
            item.SetImageIcon(skins[i].icon);
            item.SetID("H"+i);
            list.Add(item);
            item.Button.onClick.AddListener(()=>SelectItem(item.ID));
        }
    }
    
    private void Start()
    {
        List<HairSkin> hairSkins=hairDataConfigSO.HairSkins;
        GenerateScrollViewItem(hairSkins);
    }

    

    
}
