using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GloabalEnum;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponSO", order = 1)]
public class WeaponDataSO : ScriptableObject
{
    [SerializeField]private List<Weapon> weapons;
    
    public Weapon GetWeaponByEnum(WeaponEnum weaponEnum ){
        int index=(int)weaponEnum;
        for(int i=0;i<weapons.Count;i++){
            if(i==index){
                return weapons[i];
            }
        }
        return weapons[0];
    }
}
