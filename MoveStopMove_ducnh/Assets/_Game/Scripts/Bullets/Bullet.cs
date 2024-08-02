using System.Collections;
using System.Collections.Generic;
using GloabalEnum;
using UnityEngine;

namespace Bullets
{
    public class Bullet : GameUnit
    {
        private Transform target;
        private Weapon weapon;

        [SerializeField] private float speed;
        protected Vector3 startPos;
        protected Vector3 destPos;

        public void OnInit(Weapon weapon,Character target) {
            startPos=TF.position;
            this.weapon = weapon;
            this.target=target.TF;
            destPos=this.target.position+new Vector3(0,target.TF.localScale.y/2,0);
            TF.rotation.SetLookRotation(destPos);
        }

        // private void OnCollisionEnter(Collision other) {
        //     Character character=CacheCollider<Character>.GetCollider(other.collider);
        //     if(character!=target) return;
        //     SimplePool.Despawn(character);
        //     OnDespawn();

        // }

        private void OnTriggerEnter(Collider other)
        {
            Character character = CacheCollider<Character>.GetCollider(other);
            if (character == null) return;
            if(weapon.Owner.Id==character.Id)
            if (character.TF != target) return;
            SoundManager.Ins.PlaySFX(TF,ESound.WEAPON_HIT);
            if(character.OnDespawn())
                weapon.Owner.IncreaseScore(character.Score);
        }

        private void FixedUpdate()
        {
            Moving();
        }

        private void Update(){
            SpecialMove();
        }

        protected virtual void Moving()
        {
            if (target == null) return;
            if(Vector3.Distance(TF.position,destPos)<=0.01f){
                OnDespawn();
            }
            float step = speed * Time.fixedDeltaTime;
            TF.position = Vector3.MoveTowards(TF.position, destPos, step);
        }

        protected virtual void SpecialMove(){
            
        }

        public void OnDespawn()
        {
            SimplePool.Despawn(this);
            weapon.Show();
        }

    }
}
