using System;
using System.Collections;
using System.Collections.Generic;
using Bullets;
using UnityEngine;

public class Weapon : GameUnit
{
    [SerializeField] public Bullet  BulletPrefab;
    [SerializeField] public  MeshRenderer MeshRenderer;
    public Character Owner;

    public void SetPositionAndRotation(Weapon weaponPrefab){
        TF.SetLocalPositionAndRotation(weaponPrefab.TF.localPosition, weaponPrefab.TF.rotation);
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
        MeshRenderer.enabled=false;
    }

    public void Show(){
        MeshRenderer.enabled=true;
    }

    internal void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
}
