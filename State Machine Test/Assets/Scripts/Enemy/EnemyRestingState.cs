using UnityEngine;
using UnityEngine.AI;

public class EnemyRestingState : EnemyBaseState
{
    float snoozingTime;
    Rigidbody rb;
    NavMeshAgent agent;
    public override void EnterState(EnemyStateManager enemy)
    {
        snoozingTime = 5;
        enemy.transform.Rotate(0, 0, -90);

        rb = enemy.GetComponent<Rigidbody>();
        rb.constraints = ~RigidbodyConstraints.FreezeRotationZ;

        agent = enemy.GetComponent<NavMeshAgent>();
        agent.enabled = false;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if(snoozingTime >= 0)
        {
            Debug.Log("I'm resting!");
            snoozingTime -= Time.deltaTime;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationZ;
            enemy.transform.Rotate(0, 0, 90);
            enemy.SwitchState(enemy.patrolState);
        }
    }

    public override void ExitState(EnemyStateManager enemy)
    {
    }
    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {
    }

}
