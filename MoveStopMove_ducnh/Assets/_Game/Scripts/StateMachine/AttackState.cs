using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
   private float timer;
   private float randomTime;
   public void OnEnter(Bot bot)
   {
      bot.StopMoving();
      randomTime = Random.Range(2f, 4f);
   }

   public void OnExecute(Bot bot)
   {

      if (bot.Target == null || timer > 30f)
      {
         bot.ChangeState(new PatrolState());
      }
   }

   public void OnExit(Bot bot)
   {
      bot.ClearDestination();
   }
}
