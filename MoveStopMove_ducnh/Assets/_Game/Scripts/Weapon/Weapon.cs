using System;
using System.Collections;
using System.Collections.Generic;
using Bullets;
using UnityEngine;

public class Weapon : GameUnit
{
    [SerializeField] public Bullet  BulletPrefab;
    [SerializeField] private  MeshRenderer meshRenderer;
    public Character Owner;

    public void SetPositionAndRotation(Weapon weaponPrefab){
        TF.localPosition=Vector3.zero;
        TF.rotation=weaponPrefab.TF.rotation*Quaternion.Euler(new Vector3(0,0,-35f));
    }

    public void OnInit(WeaponHolder weaponHolder){
        
        Owner=weaponHolder.Owner;
    }

    public void Fire(Character target)
    {
        Bullet bullet=SimplePool.Spawn<Bullet>(BulletPrefab, TF.position, TF.rotation);
        bullet.OnInit(this,target);
        Hide();
    }

    public void Hide(){
        meshRenderer.enabled=false;
    }

    public void Show(){
        meshRenderer.enabled=true;
    }
}
