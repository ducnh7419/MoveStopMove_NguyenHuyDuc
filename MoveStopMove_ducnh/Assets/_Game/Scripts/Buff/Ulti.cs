using System.Collections;
using System.Collections.Generic;
using GlobalConstants;
using UnityEngine;

public class Ulti : GameUnit
{
    private void OnTriggerEnter(Collider other) {
        if(other.tag!=Tag.CHARACTER) return;
        Character character= CacheCollider<Character>.GetCollider(other);   
        character.ChangeUltiStatus(true); 
        OnDespawn();
    }

    private void Update() {
       TF.RotateAround(TF.position, Vector3.up, 30f * Time.deltaTime);
    }

    private void OnDespawn(){
        SimplePool.Despawn(this);
    }
}
