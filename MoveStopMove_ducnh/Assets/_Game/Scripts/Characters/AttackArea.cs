using System;

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

   public void SetAttackAreaSize(float range){
      // Scale increased by each 1 point
      TF.localScale=new Vector3(character.Range,character.Range,character.Range);
   }

   protected void OnTriggerExit(Collider other)
   {
      if (other.CompareTag(GlobalConstants.Tag.CHARACTER))
      {
         Character target = CacheCollider<Character>.GetCollider(other);
         character.TurnOffNavigator();
         if(target.Id==character.Id) return;
         character.Untarget(target);
      }
   }

   private void Update()
   {

   }


   protected virtual void CollideWithEnemy(Collider enemy)
   {
      Debug.Log(String.Format("{0} attack {1}", this.tag, enemy.tag));
      Character target = CacheCollider<Character>.GetCollider(enemy);
      if(target.Id==character.Id) return;
      target.TurnOnNavigator();
      character.AddTarget(target);
   }
}
