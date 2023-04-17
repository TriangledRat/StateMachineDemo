
using UnityEngine;

public class EnemyPatrollingState : EnemyBaseState
{
    float moveSpeed = 10;
    Rigidbody rb;
    float countdown;
    public override void EnterState(EnemyStateManager enemy)
    {
        countdown = 5;
        rb = enemy.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        rb.velocity = enemy.transform.forward * moveSpeed;
        if(countdown >= 0)
        {
            countdown -= Time.deltaTime;
        }
        else
        {
            enemy.SwitchState(enemy.restState);
        }
    }

    public override void ExitState(EnemyStateManager enemy)
    {
    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {
        GameObject other = collision.gameObject;
        if(other.tag == "Wall")
        {           
            enemy.transform.Rotate(0, -180, 0);
            rb.velocity = enemy.transform.forward * moveSpeed;
        }
    }
}
