using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    [SerializeField] public LayerMask m_LayerMask;
    public float xStore, yStore, zStore;
    public Quaternion rotationStore;
    EnemyBaseState currentState;
    public EnemyChaseState chaseState = new EnemyChaseState();
    public EnemyPatrollingState patrolState = new EnemyPatrollingState();
    public EnemyRestingState restState = new EnemyRestingState();
    public EnemyReturnState returnState = new EnemyReturnState();


    // Start is called before the first frame update
    void Start()
    {
        xStore = transform.position.x;
        yStore = transform.position.y;
        zStore = transform.position.z;
        rotationStore = transform.rotation;
        currentState = patrolState;
        currentState.EnterState(this);        
    }
    void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }


    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }


    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

}
