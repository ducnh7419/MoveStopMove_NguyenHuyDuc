using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntingState : IState<Bot>
{
    private float  timer;
    private float  randomTime;
    public void OnEnter(Bot bot)
    {
      
    }

    public void OnExecute(Bot bot)
    {
       if(bot.IsReachingDestination&&bot.Target){

       }
    }

    public void OnExit(Bot bot)
    {
        
    }
}
