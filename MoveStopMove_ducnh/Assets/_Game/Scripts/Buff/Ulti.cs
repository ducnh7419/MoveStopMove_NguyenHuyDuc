using System.Collections;
using System.Collections.Generic;
using GlobalConstants;
using UnityEngine;

public class Ulti : GameUnit
{
    private Vector3 rotatePoint;

    public void OnInit(){
        rotatePoint = TF.position;
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag!=Tag.CHARACTER) return;
        Character character= CacheCollider<Character>.GetCollider(other);   
        character.ChangeUltiStatus(true); 
        OnDespawn();
    }

    private void Update() {
       TF.RotateAround(rotatePoint, Vector3.up, 30f * Time.deltaTime);
    }

    private void OnDespawn(){
        SimplePool.Despawn(this);
    }
}
