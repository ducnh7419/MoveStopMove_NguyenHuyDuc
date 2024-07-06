using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bullets;

public class BoomerangBullet : Bullet
{

    private bool isComeBack;

    protected override void Moving()
    {
        base.Moving();
        // if (Vector3.Distance(TF.position, destPos) <= 0.01f)
        // {
        //     destPos = startPos;
        //     StartCoroutine(DelayDestroy());
        // }
    }

    protected override void SpecialMove(){
        base.SpecialMove();
        TF.rotation = TF.rotation * Quaternion.Euler(0, 0, -22.5f);
    }

    IEnumerator DelayDestroy(){
        yield return new WaitForSeconds(.5f);
        OnDespawn();
    }
}
