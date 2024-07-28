using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        bot.StopMoving();
    }

    public void OnExecute(Bot bot)
    {
        if(GameManager.Ins.CurrState==GameManager.State.StartGame||GameManager.Ins.CurrState==GameManager.State.OngoingGame)
        {
            bot.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Bot bot)
    {
    }
}
