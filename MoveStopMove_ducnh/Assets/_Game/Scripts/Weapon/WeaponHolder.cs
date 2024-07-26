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

    private void Awake() {
        
    }

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
        Weapon.SetPositionAndRotation(weaponData.WeaponPrefab);
        Material[] mats=weaponSkinData.weaponSkinPrefab.MrSkin.sharedMaterials;
        Weapon.MeshRenderer.materials=mats;
        Weapon.OnInit(this);
    }
}
