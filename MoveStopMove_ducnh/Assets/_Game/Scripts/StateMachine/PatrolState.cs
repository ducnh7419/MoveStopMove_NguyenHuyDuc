
using UnityEngine;

public class PatrolState : IState<Bot>
{
    private float timer;

    private float stateTimeLimit = 120f;

    public void OnEnter(Bot bot)
    {
        timer += Time.fixedDeltaTime;
        float radius = Random.Range(100f,150f);
        Vector3 rdnPoint = Random.insideUnitCircle * radius;
        Vector3 dest = bot.TF.position + new Vector3(rdnPoint.x, 0, rdnPoint.z);
        if(!bot.CanReachDestination(dest)){
            bot.ChangeState(new PatrolState());
            return;
        }
        bot.SetDestination(dest);
    }

    public void OnExecute(Bot bot)
    {
        if(!bot.HasDestination()) return;
        timer += Time.fixedDeltaTime;
        // if (bot.TotalTimeInCurrentState > stateTimeLimit)
        // {
        //     bot.ChangeState(new HuntingState());
        // }
        if (bot.IsReachingDestination)
        {
            Debug.Log("Reaching");
            bot.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Bot bot)
    {
        bot.ClearDestination();
        bot.SetTotalTimeInCurrState(timer);
    }

}
