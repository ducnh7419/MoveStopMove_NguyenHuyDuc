
using GloabalEnum;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public BotAttackArea botAttackArea;
    private IState<Bot> currentState;
    private float totalTimeInCurrentState;
    private NavMeshPath navMeshPathTesting;

    [Header("NavMesh Agent")]
    public NavMeshAgent Agent;
    public Vector3 destination;
    public bool IsReachingDestination => Vector3.Distance(TF.position, destination) < 0.01f;

    public float TotalTimeInCurrentState => totalTimeInCurrentState;

    protected override void Update()
    {
        base.Update();
    }

    protected override void Moving()
    {
        base.Moving();
        // Agent.enabled=true;
    }


    public bool HasDestination()
    {
        return destination != Vector3.zero;
    }

    public override void OnInit(int id)
    {
        base.OnInit(id);
        InitRandomWeapon();
        InitRandomItem();
        navMeshPathTesting = new();
        ChangeState(new IdleState());
        Score = Random.Range(0, 20);
        SetCharacterSize(Score);
        Agent.speed = speed / 2;
    }

    public bool CanReachDestination(Vector3 dest)
    {
        Agent.CalculatePath(dest, navMeshPathTesting);
        return navMeshPathTesting.status == NavMeshPathStatus.PathComplete;
    }


    public override void StopMoving()
    {
        base.StopMoving();
        // Agent.enabled=false;
        Agent.velocity = Vector3.zero;
    }

    public void SetDestination(Vector3 dest)
    {
        destination = dest;
        Moving();
        Agent.SetDestination(dest);

    }


    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (GameManager.Ins.IsState(GameManager.State.StartGame) || GameManager.Ins.IsState(GameManager.State.OngoingGame))
            currentState?.OnExecute(this);

    }

    public void ChangeState(IState<Bot> state)
    {
        currentState?.OnExit(this);
        if (currentState != null && currentState.GetType() != state.GetType())
        {
            totalTimeInCurrentState = 0;
        }
        currentState = state;
        currentState?.OnEnter(this);
    }

    public void SetTotalTimeInCurrState(float time)
    {
        totalTimeInCurrentState += time;
    }

    public void ClearDestination()
    {
        Agent.ResetPath();
        destination = Vector3.zero;
    }

    public override bool OnDespawn()
    {
        if (IsImmortal) return false;
        base.OnDespawn();
        return true;
    }
}
