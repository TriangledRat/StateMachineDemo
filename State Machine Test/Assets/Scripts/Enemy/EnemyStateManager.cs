using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{

    EnemyBaseState currentState;
    public EnemyChaseState chaseState = new EnemyChaseState();
    public EnemyPatrollingState patrolState = new EnemyPatrollingState();
    public EnemyRestingState restState = new EnemyRestingState();

    // Start is called before the first frame update
    void Start()
    {
        currentState = patrolState;
        currentState.EnterState(this);        
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
