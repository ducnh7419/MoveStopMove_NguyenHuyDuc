using System;
using System.Collections;
using System.Collections.Generic;
using GloabalEnum;
using GlobalConstants;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Character : GameUnit
{
    private int id;
    private TargetIndicator targetIndicator;
    [SerializeField] private CharacterConfigSO characterConfig;
    [SerializeField] private Transform indicatorPosition;
    protected bool isDead;
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
    protected int coin => (int)Math.Ceiling(score + score * goldBuff);
    private float goldBuff;
    public bool IsMoving { get => isMoving; }
    public bool IsAttacking;
    private bool hasUlti;
    public int Score { get => score; set => score = value; }
    public int Id { get => id; set => id = value; }
    public Character Target => target;

    public float Range => range;

    protected virtual void Update()
    {
        if (GameManager.Ins.CurrState < GameManager.State.StartGame) return;
        if(targetIndicator!=null)
            targetIndicator.SetScore(Score);
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
        target = null;
        TF.localScale = Vector3.one;
        speed = characterConfig.Speed;
        range = characterConfig.Range;
        attackSpeed = characterConfig.AttackSpeed;
        StartCoroutine(SetImmortalState(GameManager.Ins.GameRuleSO.ImmortalTime));
        targetIndicator=SimplePool.Spawn<TargetIndicator>(PoolType.Indicator);
        targetIndicator.SetTarget(indicatorPosition,FullSetSkin.MrBody.material);
        IsAttacking = false;
        navigatorRenderer.enabled = false;
        isAttackInCoolDown = false;
        isDead = false;
        ChangeUltiStatus(false);
        StopMoving();
    }

    public void SetName(string name)
    {
        this.name = name;
        targetIndicator.SetName(name);
    }

    public IEnumerator SetImmortalState(float time)
    {
        IsImmortal = true;
        yield return new WaitForSeconds(time);
        IsImmortal = false;
    }

    public void ChangeUltiStatus(bool status){
        hasUlti=status;
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
        // FullSetSkin.TakeOffSkin();
        switch (eItemType)
        {
            case EItemType.Hair:
                if (FullSetSkin.Exists()) return;
                HairSkin.InitSkin(id);
                break;
            case EItemType.Pant:
                if (FullSetSkin.Exists()) return;
                PantSkin.InitSkin(id);
                break;
            case EItemType.Shield:
                if (FullSetSkin.Exists()) return;
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
        var weaponBuff = GameManager.Ins.WeaponDataSO.GetItemBuff(weapSkinId.Item1);
        weaponHolder.Setup(weapSkinId);
        ApplyWeaponBuff(weaponBuff);
    }

    public void InitRandomWeapon()
    {
        WeaponData weaponData = GameManager.Ins.WeaponDataSO.GetRandomWeapon();
        WeaponSkinData weaponSkinData = weaponData.GetRandomSkin();
        weaponHolder.Setup(weaponData, weaponSkinData);
        var weaponBuff = GameManager.Ins.WeaponDataSO.GetItemBuff(weaponData.Id);
        ApplyWeaponBuff(weaponBuff);
    }


    private IEnumerator DelayAttack()
    {
        IsAttacking = true;
        if(hasUlti){
            ChangeAnim("ulti");
        }else{
            ChangeAnim("attack");
        }
        yield return new WaitForSeconds(.2f);
        if (Target == null)
        {
            IsAttacking = false;
            yield break;
        }
        weaponHolder.Weapon.Fire(Target,hasUlti);
        ChangeUltiStatus(false);
        SoundManager.Ins.PlaySFX(TF, ESound.ATTACK);
        IsAttacking = false;
        StartCoroutine(CoolDownAttack());
    }

    protected virtual void Attack()
    {
        if (IsAttacking) return;
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
        yield return new WaitForSeconds(2f / attackSpeed);
        isAttackInCoolDown = false;
    }

    public void RemoveTarget(Character target)
    {
        if (targets.Contains(target))
            targets.Remove(target);
    }

    public void AddUnityAction(UnityAction action)
    {
        UnityAction += action;
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
        ChangeAnim(Anim.RUN);
        target = null;
    }

    public virtual void StopMoving()
    {
        isMoving = false;
        if (IsAttacking) return;
        ChangeAnim(Anim.IDLE);
    }

    public virtual void ChangeAnim(string anim)
    {
        if (anim != currentAnim)
        {
            animator.ResetTrigger(currentAnim);
            currentAnim = anim;
            animator.SetTrigger(currentAnim);
        }
    }


    public void ApplyItemBuff()
    {
        goldBuff = 0;
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
        float defaultSpeed = characterConfig.Speed;
        float defaultRange = characterConfig.Range;
        float defaultAttackSpeed = characterConfig.AttackSpeed;
        switch (eBuffType)
        {
            case EBuffType.SpeedBuff:
                speed = defaultSpeed + defaultSpeed * value;
                break;
            case EBuffType.RangeBuff:
                range = defaultRange + defaultRange * value;
                break;
            case EBuffType.GoldBuff:
                goldBuff = value;
                break;
            case EBuffType.AttackSpeed:
                attackSpeed = defaultAttackSpeed + value;
                break;
        }
    }

    public void ApplyWeaponBuff(Tuple<EBuffType, float> weaponBuff)
    {
        float defaultSpeed = characterConfig.Speed;
        float defaultRange = characterConfig.Range;
        float defaultAttackSpeed = characterConfig.AttackSpeed;
        float value = weaponBuff.Item2;
        switch (weaponBuff.Item1)
        {
            case EBuffType.SpeedBuff:
                speed = defaultSpeed + value;
                break;
            case EBuffType.RangeBuff:
                range = defaultRange + value;
                break;
            case EBuffType.AttackSpeed:
                attackSpeed = defaultAttackSpeed + value;
                break;
        }
    }

    protected void ChangeCharacterSize(int score)
    {
        float scaleIncresed = Mathf.Log(score, 2);
        if (!Mathf.Approximately(scaleIncresed, Mathf.Round(scaleIncresed))) return;
        ParticlePool.Play(ParticleType.UpSize, TF.position + new Vector3(0, TF.position.y + TF.localScale.y / 2, 0), Quaternion.Euler(Vector3.zero));
        SoundManager.Ins.PlaySFX(TF,ESound.SIZE_UP);
        SetCharacterSize(score);
    }

    protected void SetCharacterSize(int score)
    {
        if (score == 0) return;
        float scaleIncresed = Mathf.Round(Mathf.Log(score, 2));
        TF.localScale = new Vector3(characterConfig.Size + scaleIncresed * 0.2f, characterConfig.Size + scaleIncresed * 0.2f, characterConfig.Size + scaleIncresed * 0.2f);
    }



    public virtual void IncreaseScore(int score)
    {
        Score = Score + score + GameManager.Ins.GameRuleSO.BonusPointPerKill;
        ChangeCharacterSize(score);
    }

    public void SetCharacterRange(float range)
    {
        this.range += range;
    }

    public void TurnOnNavigator()
    {
        navigatorRenderer.enabled = true;
    }

    public void TurnOffNavigator()
    {
        navigatorRenderer.enabled = false;
    }

    public virtual bool OnDespawn()
    {
        isDead = true;
        TurnOffNavigator();
        ChangeAnim(Anim.DEAD);
        if(targetIndicator!=null){
            targetIndicator.OnDesPawn();
            targetIndicator=null;
        }
        StartCoroutine(DelayDespawn());
        return true;
    }

    IEnumerator DelayDespawn()
    {
        yield return new WaitForSeconds(0.5f);
        UnityAction();
        SimplePool.Despawn(this);
        CancelInvoke();

    }
}



