using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAttackArea : AttackArea
{
    [SerializeField]private Bot bot;
    protected override void CollideWithEnemy(Collider other)
    {   
        base.CollideWithEnemy(other);
        bot.ChangeState(new AttackState());
    }

}
