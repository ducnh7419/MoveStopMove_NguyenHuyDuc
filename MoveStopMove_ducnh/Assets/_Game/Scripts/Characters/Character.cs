using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Events;

public class Character : GameUnit
{
    private int id;
    
    protected bool isMoving;
    private int score;
    public UnityAction UnityAction;
    public AttackArea attackArea;
    [SerializeField]private SpriteRenderer navigatorRenderer;
    [SerializeField] private  Animator animator;
    [SerializeField] private WeaponHolder weaponHolder;

    private List<Character> targets = new List<Character>();
    private Character target=null;
    private bool isAttackInCoolDown;
    private string currentAnim="idle";

    public bool IsMoving { get => isMoving; }
    public bool IsAttacking;
    public int Score { get => score; set => score = value; }
    public int Id { get => id; set => id = value; }

    private void Start()
    {
        OnInit();
    }

    protected virtual void Update()
    {
        if (!isMoving)
        {
            Attack();
        }
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(1f);
        if(isMoving) yield break;
        ChangeAnim("attack");
        weaponHolder.Weapon.Fire(target);
        weaponHolder.Weapon.Hide();      
    }

    // public void ResetTargets()
    // {
    //     targets.Clear();
    //     target = null;
    // }

    private IEnumerator CoolDownAttack(){
        yield return new WaitForSeconds(5f);
        isAttackInCoolDown=false;
        IsAttacking=false;
    }

    public void RemoveDespawnTarget()
    {
        targets.Remove(target);
    }

    public void RemoveTarget(Character target){
        if(targets.Contains(target))        
            target.UnityAction-=()=>Untarget(target);
            targets.Remove(target);
    }

    private void Untarget(Character target){
        if(this.target.id== target.id){
            this.target = null;
        }
        
    }


    public void AddTarget(Character target)
    {
        if(!targets.Contains(target)){
            targets.Add(target);
            target.UnityAction +=()=> Untarget(target);
        }
    }

    protected virtual void LockTarget()
    {
        if (targets.Count <= 0) return;
        float minDist = Vector3.Distance(TF.position, targets[0].TF.position);
        target = targets[0];
        for (int i = 1; i < targets.Count; i++)
        {
            if (Vector3.Distance(TF.position, targets[i].TF.position) < minDist)
            {
                    target = targets[i];

            }
        }
    }
    
    protected virtual void Moving()
    {
        isMoving = true;
        ChangeAnim("run");
        // ResetTargets();
        attackArea.TurnOnCollider();
    }

    protected virtual void StopMoving()
    {
        isMoving = false;
        // if(!IsAttacking)
        ChangeAnim("idle");
    }

    protected virtual void ChangeAnim(string anim){
        if(anim!=currentAnim){
            currentAnim=anim;
            animator.SetTrigger(anim);       
        }
    }

    protected virtual void OnInit()
    {
        targets = new List<Character>();
    }

    protected virtual void Attack()
    {
        if(isAttackInCoolDown) return;
        LockTarget();   
        if (target == null) return;
        StartCoroutine(DelayAttack());
        IsAttacking=true;
        isAttackInCoolDown =true;
        StartCoroutine(CoolDownAttack());
    }

    public void TurnOnNavigator(){
        navigatorRenderer.enabled=true;
    }

    public void TurnOffNavigator(){
        navigatorRenderer.enabled=false;
    }

    public void OnDespawn(){
        UnityAction();
        SimplePool.Despawn(this);
    }
}

