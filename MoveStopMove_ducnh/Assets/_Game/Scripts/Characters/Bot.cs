
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    private IState<Bot> currentState;
    private float totalTimeInCurrentState;
    private List<Character> trackedEnemy;
    public int NumberOfTrackedEnemies=>TrackedEnemy.Count;

    public List<Character> TrackedEnemy => trackedEnemy;

    [Header("NavMesh Agent")]
    public NavMeshAgent Agent;
    public Vector3 destination;
    public bool IsReachingDestination => Vector3.Distance(TF.position, destination)< .01f;

    public float TotalTimeInCurrentState => totalTimeInCurrentState;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public override void OnInit(int id){
        base.OnInit(id);
        ChangeState(new IdleState());
        Score=Random.Range(10,51);
        attackArea.SetAttackAreaSize(Score);
        Agent.stoppingDistance=2f;
    }

    public void TrackEnemy(Character character){
        TrackedEnemy.Add(character);
    }

    public void SetDestination(Vector3 dest){
        Agent.SetDestination(dest);
    }
    

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        currentState?.OnExecute(this);
    }

    public void ChangeState(IState<Bot> state)
    {
        currentState?.OnExit(this);
        if(currentState.GetType() != state.GetType()){
            totalTimeInCurrentState=0;
        }
        currentState = state;
        currentState?.OnEnter(this);
    }

    public void SetTotalTimeInCurrState(float time){
        totalTimeInCurrentState+=time;
    }

    public void ClearDestination()
    {
        Agent.ResetPath();
    }
}
