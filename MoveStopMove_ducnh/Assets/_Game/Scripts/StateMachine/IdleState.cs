using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    private float  timer;
    private float  randomTime;
    public void OnEnter(Bot bot)
    {
        timer=0;
        randomTime=Random.Range(1f, 3f);
    }

    public void OnExecute(Bot bot)
    {
        timer+=Time.fixedDeltaTime;
        if(timer>randomTime){
            bot.ChangeState(new PatrolState());
            
        }
    }

    public void OnExit(Bot bot)
    {
        
    }
}
