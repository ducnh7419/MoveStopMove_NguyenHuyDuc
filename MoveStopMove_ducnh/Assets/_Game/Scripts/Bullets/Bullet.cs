using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bullets
{
    public class Bullet : GameUnit
    {
        private Transform target;
        public Character Owner;
        private Weapon weapon;

        [SerializeField] private float speed;
        protected Vector3 startPos;
        protected Vector3 destPos;

        public Transform Target { set => target = value; }
        public Weapon Weapon { get => weapon; set => weapon = value; }

        private void Start() {
            startPos=TF.position;
            destPos=target.position+new Vector3(0,1f,0);
        }

        // private void OnCollisionEnter(Collision other) {
        //     Character character=CacheCollider<Character>.GetCollider(other.collider);
        //     if(character!=target) return;
        //     SimplePool.Despawn(character);
        //     OnDespawn();

        // }

        private void OnTriggerEnter(Collider other)
        {
            Character character = CacheCollider<Character>.GetCollider(other.GetComponent<Collider>());
            if (character == null) return;
            if (character.TF != target) return;
            character.OnDespawn();
            OnDespawn();
            Owner.Score+=character.Score;
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
