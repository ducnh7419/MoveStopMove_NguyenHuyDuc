using System;
using System.Collections;
using System.Collections.Generic;
using Bullets;
using UnityEngine;

public class Weapon : GameUnit
{
    [SerializeField] public Bullet  BulletPrefab;
    [SerializeField] public  MeshRenderer MeshRenderer;
    private float bulletSpeed;
    public Character Owner;

    public void SetPositionAndRotation(Weapon weaponPrefab){
        TF.SetLocalPositionAndRotation(weaponPrefab.TF.localPosition, weaponPrefab.TF.rotation);
    }

    public void SkinSetUp(WeaponSkinData weaponSkinData){
        Material[] mats=weaponSkinData.weaponSkinPrefab.MrSkin.sharedMaterials;
        MeshRenderer.materials=mats;
        
    }

    public void OnInit(WeaponHolder weaponHolder,float bulletSpeed){
        
        Owner=weaponHolder.Owner;
        this.bulletSpeed=bulletSpeed;
    }

    public float GetBulletSpeed(){
        return this.bulletSpeed;
    }

    public void Fire(Character target,bool isUlti)
    {
        Bullet bullet=SimplePool.Spawn<Bullet>(BulletPrefab, TF.position, TF.rotation);
        bullet.OnInit(this,target);
        if(isUlti){
            bullet.SetScale(bullet.TF.localScale*5);
        }else{
            bullet.SetScale(BulletPrefab.TF.localScale);
        }
        bullet.SkinSetup(MeshRenderer.materials);
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
