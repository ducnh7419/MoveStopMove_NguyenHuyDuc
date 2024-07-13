using System.Collections;
using System.Collections.Generic;
using GloabalEnum;
using UnityEngine;

[CreateAssetMenu(fileName = "SetFullSkinDataConfig", menuName = "ScriptableObjects/SetFullSkinDataConfigSO", order = 6)]
public class SetFullSkinDataConfigSO : ScriptableObject
{
    [SerializeField] private List<SetFullSkin> setFullSkins;

    public List<SetFullSkin> SetFullSkins => setFullSkins;

    public Skin GetHairSkinByEnum(SetFullEnum setFullEnum){
        int index=(int)setFullEnum;
         for(int i=0;i<setFullSkins.Count;i++){
            if(i==index){
                return setFullSkins[i];
            }
        }
        return setFullSkins[0];
    }

    public Skin GetRandomHairSkin(){
        int rdn=Random.Range(0, setFullSkins.Count);
        return setFullSkins[rdn];
    }

    

}
