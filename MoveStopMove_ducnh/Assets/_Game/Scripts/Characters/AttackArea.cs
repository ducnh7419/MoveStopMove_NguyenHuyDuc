using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
   [SerializeField] Collider areaCollider;
   [SerializeField] Character character;

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag(GlobalConstants.Tag.CHARACTER))
      {
         CollideWithEnemy(other);
      }
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.CompareTag(GlobalConstants.Tag.CHARACTER))
      {
         Character target = CacheCollider<Character>.GetCollider(other);
         character.RemoveTarget(target);
      }
   }



   public void TurnOnCollider()
   {
      areaCollider.enabled = true;
   }

   public void TurnOffCollider()
   {
      areaCollider.enabled = false;
   }

   private void Update()
   {
   }


   private void CollideWithEnemy(Collider enemy)
   {
      Debug.Log(String.Format("{0} attack {1}", this.tag, enemy.tag));
      Character target = CacheCollider<Character>.GetCollider(enemy);
      character.AddTarget(target);
   }
}
