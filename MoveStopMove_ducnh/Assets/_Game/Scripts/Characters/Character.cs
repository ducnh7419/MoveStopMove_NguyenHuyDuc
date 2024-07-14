using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Events;

public class Character : GameUnit
{
    private int id;
    [SerializeField] private CharacterConfigSO characterConfig;
    protected float speed;
    protected bool isMoving;
    protected float attackSpeed=1f;
    private int score;
    public CharacterSkin characterSkin;
    public UnityAction UnityAction;
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
    public Character Target => target;

    protected virtual void Update(){
        if(GameManager.Ins.CurrState<GameManager.State.StartGame) return;
        GameManager.Ins.SetCharacterScore(this,score);
    }

    protected virtual void FixedUpdate()
    {
        if(GameManager.Ins.CurrState<GameManager.State.StartGame) return;
        if (!isMoving)
        {
            Attack();
        }
        
    }

    public virtual void InitRandomItem(){
        characterSkin.InitRandomItem();
    }

    public virtual void InitFullSetSkin(int id){
        characterSkin.InitFullSetSkin(id);
    }

    

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(attackSpeed);
        IsAttacking=false;
        if(Target==null) yield break;
        ChangeAnim("attack");
        weaponHolder.Weapon.Fire(Target);
        isAttackInCoolDown =true;
        StartCoroutine(CoolDownAttack());
    }

    // public void ResetTargets()
    // {
    //     targets.Clear();
    //     target = null;
    // }

    private IEnumerator CoolDownAttack(){
        yield return new WaitForSeconds(2f);
        isAttackInCoolDown=false;
    }

    public void RemoveTarget(Character target){
        if(targets.Contains(target))        
            targets.Remove(target);
    }

    public void DropUnityActionEvent(Character target){
        target.UnityAction-=()=>Untarget(target);
    }

    public void Untarget(Character target){
        RemoveTarget(target);
        if(this.target==null) return;
        if(this.target.id== target.id){
            this.target = null;
        }
    }


    public void AddTarget(Character target)
    {
        if(!targets.Contains(target)){
            targets.Add(target);
            target.UnityAction+=()=>Untarget(target);
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
        target=null;
    }

    public virtual void StopMoving()
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

    public virtual void OnInit(int id)
    {
        Id=id;
        UnityAction=null;
        targets = new List<Character>();
        speed=characterConfig.Speed;
        StopMoving();
        
    }

    public virtual void IncreaseScore(int score){
        Score+=score;
        
    }

    protected virtual void Attack()
    {
        if(IsAttacking) return;
        if(isAttackInCoolDown) return;
        LockTarget();   
        if (Target == null) return;
        Vector3 direction=Target.TF.position-TF.position;
        TF.rotation=Quaternion.LookRotation(direction);
        StartCoroutine(DelayAttack());
        IsAttacking=true;
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



