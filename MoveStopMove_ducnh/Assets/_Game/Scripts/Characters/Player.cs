using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    public DynamicJoystick Joystick;
    [SerializeField] private Rigidbody rb;
    public AttackArea attackArea;
    public int Coin;


    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (Joystick.Horizontal != 0 || Joystick.Vertical != 0)
        {
            rb.velocity = new Vector3(Joystick.Horizontal * speed * Time.fixedDeltaTime, 0, Joystick.Vertical * speed * Time.fixedDeltaTime);
            TF.rotation = Quaternion.LookRotation(rb.velocity);
            Moving();
        }else{
            StopMoving();
            Debug.Log(IsAttacking);
        }
    }


    public override void OnInit(int id)
    {
        base.OnInit(id);
        Score=0;
        attackArea.SetAttackAreaSize(Score);
        UserDataManager.Ins.InitEquippedWeapon();
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

    
}
