
using GloabalEnum;
using GlobalConstants;
using UnityEngine;

public class Player : Character
{
    public DynamicJoystick Joystick;
    [SerializeField] private Rigidbody rb;
    public AttackArea attackArea;
    public bool CanRevive;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(GameManager.Ins.IsState(GameManager.State.SkinShop)){
            ChangeAnim(Anim.SKIN_DANCE);
        }
        if(isDead) return;
        if(Joystick==null) return;
        if (Joystick.Horizontal != 0 || Joystick.Vertical != 0)
        {
            Moving();
            rb.velocity = new Vector3(Joystick.Horizontal * speed * Time.fixedDeltaTime, 0, Joystick.Vertical * speed * Time.fixedDeltaTime);
            TF.rotation = Quaternion.LookRotation(rb.velocity);
        }else{
            StopMoving();
        }
    }


    public override void OnInit(int id)
    {
        base.OnInit(id);
        CanRevive=true;
        Score=0;
        attackArea.SetAttackAreaSize(Score);
        UserDataManager.Ins.InitEquippedWeapon();
        UserDataManager.Ins.LoadAllEquippedItem();
    }


    public void SetJoyStickController(DynamicJoystick joystick){
        this.Joystick=joystick;
    }

    public override void StopMoving()
    {
        base.StopMoving();
        rb.velocity=Vector3.zero;
    }

    public override void IncreaseScore(int score)
    {
        base.IncreaseScore(score);
        attackArea.SetAttackAreaSize(Score);
    }

    public int GetCoin(){
        return coin;
    }

    public override bool OnDespawn()
    {
        if(IsImmortal) return false;
        SoundManager.Ins.PlaySFX(ESound.PLAYER_DEATH);
        base.OnDespawn();
        return true;
        
    }

}
