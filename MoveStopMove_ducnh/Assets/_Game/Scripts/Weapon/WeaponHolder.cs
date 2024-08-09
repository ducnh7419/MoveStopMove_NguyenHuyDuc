using System;
using System.Collections;
using System.Collections.Generic;
using Bullets;
using UnityEngine;

public class WeaponHolder : GameUnit
{


    [NonSerialized] public Weapon Weapon;
    [SerializeField]private Character owner;
    public Character Owner { get => owner;}

    public void Setup(Tuple<int,int> weapSkinId) {
        if(Weapon!=null){
            Weapon.OnDespawn();
        }     
        WeaponData weaponData=GameManager.Ins.WeaponDataSO.GetWeaponDataById(weapSkinId.Item1);
        WeaponSkinData weaponSkinData=weaponData.GetWeaponSkinById(weapSkinId.Item2);
        Setup(weaponData,weaponSkinData);
    }

    public void Setup(WeaponData weaponData,WeaponSkinData weaponSkinData){
        if(Weapon!=null){
            Weapon.OnDespawn();
        }
        Weapon=SimplePool.Spawn<Weapon>(weaponData.WeaponPrefab,TF);
        Weapon.OnInit(this,weaponData.bulletSpeed);
        Weapon.SetPositionAndRotation(weaponData.WeaponPrefab);
        Weapon.SkinSetUp(weaponSkinData);
    }
}
