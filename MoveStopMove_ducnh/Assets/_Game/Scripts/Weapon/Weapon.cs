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

    public void Fire(Character target)
    {
        Bullet bullet=SimplePool.Spawn<Bullet>(BulletPrefab, TF.position, TF.rotation);
        bullet.Target = target.TF;
        bullet.Weapon = this;
        bullet.Owner=Owner;
    }

    public void Hide(){
        meshRenderer.enabled=false;
    }

    public void Show(){
        meshRenderer.enabled=true;
    }
}
