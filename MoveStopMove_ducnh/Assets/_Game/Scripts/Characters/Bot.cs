
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public BotAttackArea botAttackArea;
    private IState<Bot> currentState;
    private float totalTimeInCurrentState;
    [SerializeField] private Target targetIndicator;
    private NavMeshPath navMeshPathTesting;

    [Header("NavMesh Agent")]
    public NavMeshAgent Agent;
    public Vector3 destination;
    public bool IsReachingDestination => Vector3.Distance(TF.position, destination)< 0.01f;

    public float TotalTimeInCurrentState => totalTimeInCurrentState;

    protected override void Update(){
        base.Update();
    }


    public bool HasDestination(){
        return destination!=Vector3.zero;
    }

    public override void OnInit(int id){
        base.OnInit(id);
        navMeshPathTesting= new();
        InitRandomItem();
        InitRandomWeapon();
        ChangeState(new IdleState());
        Score=Random.Range(10,20);
        botAttackArea.SetAttackAreaSize(Score);
        Agent.speed=speed/2;
    }

    public bool CanReachDestination(Vector3 dest){
        Agent.CalculatePath(dest,navMeshPathTesting);
        return navMeshPathTesting.status==NavMeshPathStatus.PathComplete;
    }


   public override void StopMoving(){
        base.StopMoving();
        Agent.velocity=Vector3.zero;
        ClearDestination();
   }

    public void SetDestination(Vector3 dest){
        destination=dest;
        Moving();
        Agent.SetDestination(dest);
        
    }
    

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        currentState?.OnExecute(this);
        
    }

    public void ChangeState(IState<Bot> state)
    {
        currentState?.OnExit(this);
        if(currentState!=null&&currentState.GetType() != state.GetType()){
            totalTimeInCurrentState=0;
        }
        currentState = state;
        currentState?.OnEnter(this);
    }

    public void SetTotalTimeInCurrState(float time){
        totalTimeInCurrentState+=time;
    }

    public override void IncreaseScore(int score)
    {
        base.IncreaseScore(score);
        botAttackArea.SetAttackAreaSize(Score);
    }

    public void Hunting(){
        Character target=GameManager.Ins.GetCharacterHaveHighestScore();
        destination=target.TF.position;
        SetDestination(destination);
    }

    

    public void ClearDestination()
    {
        Agent.ResetPath();
        destination=Vector3.zero;
    }

    public override void OnDespawn(){
        if(IsImmortal) return;
        LevelManager.Ins.DecreseNORemainBots();
        base.OnDespawn();
    }
}
