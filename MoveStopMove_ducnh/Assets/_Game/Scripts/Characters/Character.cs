using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GloabalEnum;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Events;

public class Character : GameUnit
{
    private int id;
    [SerializeField] private CharacterConfigSO characterConfig;
    protected float speed;
    private float range;
    public bool IsImmortal;
    protected bool isMoving;
    //attack per second
    protected float attackSpeed;
    private int score;
    public HairSkinImpl HairSkin;
    public PantSkinImpl PantSkin;
    public ShieldSkinImpl ShieldSkin;
    public FullSetImpl FullSetSkin;
    public UnityAction UnityAction;
    [SerializeField] private SpriteRenderer navigatorRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private WeaponHolder weaponHolder;
    private List<Character> targets = new List<Character>();
    private Character target = null;
    private bool isAttackInCoolDown;
    private string currentAnim = "idle";
    protected int coin => (int)Math.Ceiling(score+score*goldBuff);
    private float goldBuff;
    public bool IsMoving { get => isMoving; }
    public bool IsAttacking;
    public int Score { get => score; set => score = value; }
    public int Id { get => id; set => id = value; }
    public Character Target => target;

    public float Range => range;

    protected virtual void Update()
    {
        if (GameManager.Ins.CurrState < GameManager.State.StartGame) return;
        GameManager.Ins.SetCharacterScore(this, score);
    }

    protected virtual void FixedUpdate()
    {
        if (GameManager.Ins.CurrState < GameManager.State.StartGame) return;
        if (!isMoving)
        {
            Attack();
        }

    }

    public virtual void OnInit(int id)
    {
        Id = id;
        UnityAction = null;
        targets = new List<Character>();
        target=null;
        speed = characterConfig.Speed;
        range = characterConfig.Range;
        attackSpeed=characterConfig.AttackSpeed;
        StopMoving();
        StartCoroutine(SetImmortalState(GameManager.Ins.GameRuleSO.ImmortalTime));
        IsAttacking=false;
        isAttackInCoolDown=false;

    }

    public IEnumerator SetImmortalState(float time){
        IsImmortal=true;
        yield return new WaitForSeconds(time);
        IsImmortal=false;
    }


    public virtual void InitRandomItem()
    {
        HairSkin.InitRandomItem();
        PantSkin.InitRandomItem();
        ShieldSkin.InitRandomItem();
        ApplyItemBuff();
    }

    public virtual void ChangeSkin(int id, EItemType eItemType)
    {
        switch (eItemType)
        {
            case EItemType.Hair:
                HairSkin.InitSkin(id);
                break;
            case EItemType.Pant:
                PantSkin.InitSkin(id);
                break;
            case EItemType.Shield:
                ShieldSkin.InitSkin(id);
                break;
            case EItemType.FullSet:
                HairSkin.InitSkin(0);
                PantSkin.InitSkin(0);
                ShieldSkin.InitSkin(0);
                FullSetSkin.InitSkin(id);
                break;
        }
        ApplyItemBuff();
    }

    public void InitWeapon(Tuple<int, int> weapSkinId)
    {
        var weaponBuff=GameManager.Ins.WeaponDataSO.GetItemBuff(weapSkinId.Item1); 
        weaponHolder.Setup(weapSkinId);
        ApplyWeaponBuff(weaponBuff);
    }

    public void InitRandomWeapon()
    {
        WeaponData weaponData = GameManager.Ins.WeaponDataSO.GetRandomWeapon();
        WeaponSkinData weaponSkinData = weaponData.GetRandomSkin();
        weaponHolder.Setup(weaponData, weaponSkinData);
        var weaponBuff=GameManager.Ins.WeaponDataSO.GetItemBuff(weaponData.Id); 
        ApplyWeaponBuff(weaponBuff);
    }


    private IEnumerator DelayAttack()
    {
        IsAttacking=true;
        ChangeAnim("attack");
        yield return new WaitForSeconds(.5f);
        if (Target == null) yield break;
        weaponHolder.Weapon.Fire(Target);
        IsAttacking = false;
        StartCoroutine(CoolDownAttack());
    }

    protected virtual void Attack()
    {
        if(IsAttacking) return;
        if (isAttackInCoolDown) return;
        LockTarget();
        if (Target == null) return;
        Vector3 direction = Target.TF.position - TF.position;
        TF.rotation = Quaternion.LookRotation(direction);
        StartCoroutine(DelayAttack());
        IsAttacking = true;
    }

    private IEnumerator CoolDownAttack()
    {
        isAttackInCoolDown = true;
        yield return new WaitForSeconds(2f/attackSpeed);
        isAttackInCoolDown = false;
    }

    public void RemoveTarget(Character target)
    {
        if (targets.Contains(target))
            targets.Remove(target);
    }

    public void DropUnityActionEvent(Character target)
    {
        target.UnityAction -= () => Untarget(target);
    }

    public void Untarget(Character target)
    {
        RemoveTarget(target);
        if (this.target == null) return;
        if (this.target.id == target.id)
        {
            this.target = null;
        }
    }


    public void AddTarget(Character target)
    {
        if (!targets.Contains(target))
        {
            targets.Add(target);
            target.UnityAction += () => Untarget(target);
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
        target = null;
    }

    public virtual void StopMoving()
    {
        isMoving = false;
        ChangeAnim("idle");
    }

    public virtual void ChangeAnim(string anim)
    {
        if (anim != currentAnim)
        {
            currentAnim = anim;
            animator.SetTrigger(anim);
        }
    }

    
    public void ApplyItemBuff()
    {
        goldBuff=0;
        if (HairSkin.Exists())
        {
            var itemBuff = HairSkin.GetItemBuff();
            ApplyItemBuffByType(itemBuff.Item1, itemBuff.Item2);
        }
        if (PantSkin.Exists())
        {
            var itemBuff = PantSkin.GetItemBuff();
            ApplyItemBuffByType(itemBuff.Item1, itemBuff.Item2);
        }
        if (FullSetSkin.Exists())
        {
            var itemBuff = FullSetSkin.GetItemBuff();
            ApplyItemBuffByType(itemBuff.Item1, itemBuff.Item2);
        }
    }

    public void ApplyItemBuffByType(EBuffType eBuffType, float value)
    {
        switch (eBuffType)
        {
            case EBuffType.SpeedBuff:
                speed += speed * value;
                break;
            case EBuffType.RangeBuff:
                range += range * value;
                break;
            case EBuffType.GoldBuff:
                goldBuff=value;
                break;
            case EBuffType.AttackSpeed:
                attackSpeed+=value;
                break;
        }
    }

    public void ApplyWeaponBuff(Tuple<EBuffType,float> weaponBuff)
    {
        float value=weaponBuff.Item2;
        switch (weaponBuff.Item1)
        {
            case EBuffType.SpeedBuff:
                speed += value;
                break;
            case EBuffType.RangeBuff:
                range += value;
                break;
            
        }
    }
    
    public virtual void IncreaseScore(int score)
    {
        Score = Score+score+GameManager.Ins.GameRuleSO.BonusPointPerKill;

    }

    

    public void TurnOnNavigator()
    {
        navigatorRenderer.enabled = true;
    }

    public void TurnOffNavigator()
    {
        navigatorRenderer.enabled = false;
    }

    public virtual void OnDespawn()
    {
        ChangeAnim("dead");
        StartCoroutine(DelayDespawn());
    }

    IEnumerator DelayDespawn()
    {
        yield return new WaitForSeconds(0.5f);
        SimplePool.Despawn(this);
        UnityAction();

    }
}



