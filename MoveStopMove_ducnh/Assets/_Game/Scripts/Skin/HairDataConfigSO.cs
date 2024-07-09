using System.Collections;
using System.Collections.Generic;
using GloabalEnum;
using UnityEngine;

[CreateAssetMenu(fileName = "HairDataConfig", menuName = "ScriptableObjects/HairDataConfigSO", order = 3)]
public class HairDataConfigSO : ScriptableObject
{
    [SerializeField] private List<Skin> hairSkins;

    public Skin GetHairSkinByEnum(HairSkinEnum hairSkinEnum){
        int index=(int)hairSkinEnum;
         for(int i=0;i<hairSkins.Count;i++){
            if(i==index){
                return hairSkins[i];
            }
        }
        return hairSkins[0];
    }

    public Skin GetRandomHairSkin(){
        int rdn=Random.Range(0, hairSkins.Count);
        return hairSkins[rdn];
    }

}
