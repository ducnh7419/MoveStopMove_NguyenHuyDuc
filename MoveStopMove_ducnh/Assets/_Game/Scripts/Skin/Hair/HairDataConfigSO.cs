using System.Collections;
using System.Collections.Generic;
using GloabalEnum;
using UnityEngine;

[CreateAssetMenu(fileName = "HairDataConfig", menuName = "ScriptableObjects/HairDataConfigSO", order = 3)]
public class HairDataConfigSO : ScriptableObject
{
    [SerializeField] private List<HairSkin> hairSkins;

    public List<HairSkin> HairSkins => hairSkins;

    public Skin GetHairSkinByEnum(HairSkinEnum hairSkinEnum){
        int index=(int)hairSkinEnum;
         for(int i=0;i<HairSkins.Count;i++){
            if(i==index){
                return HairSkins[i];
            }
        }
        return HairSkins[0];
    }

    public Skin GetRandomHairSkin(){
        int rdn=Random.Range(0, HairSkins.Count);
        return HairSkins[rdn];
    }

    

}
