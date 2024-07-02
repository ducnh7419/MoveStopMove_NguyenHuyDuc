using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    private IState<Bot> currentState;
    private List<Character> trackedEnemy;
    public int NumberOfTrackedEnemies=>TrackedEnemy.Count;

    public List<Character> TrackedEnemy => trackedEnemy;

    [Header("NavMesh Agent")]
    public NavMeshAgent Agent;
    public Vector3 destination;


    // Start is called before the first frame update
    void Start()
    {
        Score=20;
        Agent.stoppingDistance=2f;
    }

    public void TrackEnemy(Character character){
        TrackedEnemy.Add(character);
    }

    public void SetDestination(Character character){
        Agent.SetDestination(character.TF.position);
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

        currentState = state;

        currentState?.OnEnter(this);
    }
}
