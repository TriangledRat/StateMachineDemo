using UnityEngine;

public class EnemyRestingState : EnemyBaseState
{
    float snoozingTime;
    Rigidbody rb;
    public override void EnterState(EnemyStateManager enemy)
    {
        snoozingTime = 5;
        rb = enemy.GetComponent<Rigidbody>();
        rb.constraints = ~RigidbodyConstraints.FreezeRotationZ;
        enemy.transform.Rotate(0, 0, -90);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if(snoozingTime >= 0)
        {
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
