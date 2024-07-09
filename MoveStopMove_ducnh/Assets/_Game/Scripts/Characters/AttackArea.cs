using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
   [SerializeField] private CharacterConfigSO characterConfigSO;
   [SerializeField] private Collider areaCollider;
   [SerializeField] private Transform TF;
   [SerializeField] private Character character;

   private void OnTriggerEnter(Collider other)
   {
      
      if (other.CompareTag(GlobalConstants.Tag.CHARACTER))
      {
         CollideWithEnemy(other);
      }
   }

   public void SetAttackAreaSize(int score){
      // Scale increased by each 1 point
      float scaleEachScore=0.1f;
      float numberOfTimeIncreased=score*scaleEachScore;
      TF.localScale=characterConfigSO.DefaultAtackAreaScale+new Vector3(numberOfTimeIncreased,0,numberOfTimeIncreased);
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.CompareTag(GlobalConstants.Tag.CHARACTER))
      {
         Character target = CacheCollider<Character>.GetCollider(other);
         if(target.Id==character.Id) return;
         character.TurnOffNavigator();
         character.Untarget(target);
      }
   }

   private void Update()
   {

   }


   private void CollideWithEnemy(Collider enemy)
   {
      Debug.Log(String.Format("{0} attack {1}", this.tag, enemy.tag));
      Character target = CacheCollider<Character>.GetCollider(enemy);
      if(target.Id==character.Id) return;
      target.TurnOnNavigator();
      character.AddTarget(target);
   }
}
