using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Bot>
{
    private float randomTime;
    private float timer;

    public void OnEnter(Bot bot)
    {
       randomTime=Random.Range(2f, 4f);
    }

    public void OnExecute(Bot bot)
    {
        timer+=randomTime;
        if(timer>60f){
            
        }
    }

    public void OnExit(Bot bot)
    {
        throw new System.NotImplementedException();
    }

}
