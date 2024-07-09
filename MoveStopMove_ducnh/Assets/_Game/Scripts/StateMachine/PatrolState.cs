using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PatrolState : IState<Bot>
{
    private float randomTime;
    private float timer;

    private float stateTimeLimit=120f;

    public void OnEnter(Bot bot)
    {
       randomTime=Random.Range(2f, 4f);
       float radius=5;
       Vector3 rdnPoint=Random.insideUnitCircle*radius;
       Vector3 dest=bot.TF.position+new Vector3(rdnPoint.x,0,rdnPoint.z);
       bot.SetDestination(dest);
    }

    public void OnExecute(Bot bot)
    {
        timer+=randomTime;
        if(bot.TotalTimeInCurrentState>stateTimeLimit){
            bot.ChangeState(new HuntingState());
        }
        if(bot.IsReachingDestination){
            bot.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Bot bot)
    {
        bot.ClearDestination();
        bot.SetTotalTimeInCurrState(timer);
    }

}
