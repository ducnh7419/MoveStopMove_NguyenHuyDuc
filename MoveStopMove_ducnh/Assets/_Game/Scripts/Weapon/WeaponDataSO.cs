using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GloabalEnum;
using Unity.VisualScripting;
using System;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponDataSO", order = 3)]
public class WeaponDataSO : ScriptableObject
{
    public List<WeaponData> weapons=new();
    
    public WeaponData GetWeaponDataById(int id ){
        for(int i=0;i<weapons.Count;i++){
            if(weapons[i].Id==id){
                return weapons[i];
            }
        }
        return null;
    }
    
}

[Serializable]
public class WeaponData{
    public int Id;
    public string Name;
    public string Description;
    public Sprite Icon;
    public int Price;
    public Weapon WeaponPrefab;
}
