using System;
using System.Collections;
using System.Collections.Generic;
using Bullets;
using UnityEngine;

public class WeaponHolder : GameUnit
{

    public WeaponDataSO WeaponDataSO;
    [NonSerialized] public Weapon Weapon;
    [SerializeField]private Character owner;
    public Character Owner { get => owner;}

    private void Awake() {
        
    }

    private void Start() {     
        Weapon WeaponPrefab= WeaponDataSO.GetWeaponDataById(0).WeaponPrefab;
        Weapon=SimplePool.Spawn<Weapon>(WeaponPrefab,TF);
        Weapon.SetPositionAndRotation(WeaponPrefab);
        Weapon.OnInit(this);
    }
}
