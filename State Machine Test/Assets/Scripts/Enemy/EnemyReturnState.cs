
using UnityEngine;
using UnityEngine.AI;

public class EnemyReturnState : EnemyBaseState
{
    Rigidbody rb;
    Vector3 returnTarget;
    NavMeshAgent agent;


    public override void EnterState(EnemyStateManager enemy)
    {
        returnTarget = new Vector3(enemy.xStore, enemy.yStore, enemy.zStore);
        rb = enemy.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        agent = enemy.GetComponent<NavMeshAgent>();
        agent.enabled = true;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {      
        if(enemy.transform.position.x == returnTarget.x)
        {
            rb.constraints = ~RigidbodyConstraints.FreezeRotation;
            enemy.transform.rotation = enemy.rotationStore;
            enemy.transform.position = returnTarget;
            enemy.SwitchState(enemy.patrolState);
        }
        else
        {
            Debug.Log("I'm going back!");           
            agent.SetDestination(returnTarget);
        }
    }

    public override void ExitState(EnemyStateManager enemy)
    {
    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {
    }

}
