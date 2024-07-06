using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    public DynamicJoystick Joystick;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    private void FixedUpdate()
    {
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

    protected override void StopMoving()
    {
        base.StopMoving();
        rb.velocity=Vector3.zero;
    }
}
