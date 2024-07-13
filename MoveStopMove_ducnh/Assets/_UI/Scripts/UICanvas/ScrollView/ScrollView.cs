using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollView : UICanvas
{
    protected int id;
    protected List<ScrollViewItem> list=new();
    public Transform parentContent;
    [SerializeField]protected ScrollViewItem scrollViewItem;
   
   

   public void SelectItem(int id){
        Debug.Log("Clicked");
        for (int i=0;i<list.Count;i++){
            if(list[i].ID==id){
                list[i].ChangeSelectedStatus(true);
            }else{
                list[i].ChangeSelectedStatus(false);
            }
        }
    }
}
