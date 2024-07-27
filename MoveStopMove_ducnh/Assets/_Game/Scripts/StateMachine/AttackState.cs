using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
       bot.StopMoving();
    }

    public void OnExecute(Bot bot)
    {
       if(bot.Target==null){
            bot.ChangeState(new PatrolState());
       }
    }

    public void OnExit(Bot bot)
    {
       bot.ClearDestination();
    }
}
