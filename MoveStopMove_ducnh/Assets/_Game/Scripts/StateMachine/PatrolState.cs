using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        int rdn=Random.Range(0, bot.NumberOfTrackedEnemies);
        bot.SetDestination(bot.TrackedEnemy[rdn]);
    }

    public void OnExecute(Bot bot)
    {
        throw new System.NotImplementedException();
    }

    public void OnExit(Bot bot)
    {
        throw new System.NotImplementedException();
    }

}
