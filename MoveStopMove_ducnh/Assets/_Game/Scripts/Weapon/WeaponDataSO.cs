using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponSO", order = 1)]
public class WeaponDataSO : ScriptableObject
{
    [SerializeField]private List<Weapon> weapons;
    
    public Weapon GetWeaponByEnum(int index){
        for(int i=0;i<weapons.Count;i++){
            if(i==index){
                return weapons[i];
            }
        }
        return weapons[0];
    }
}
