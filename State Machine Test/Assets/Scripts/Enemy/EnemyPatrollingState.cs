
using UnityEngine;

public class EnemyPatrollingState : EnemyBaseState
{
    Vector3 moveSpeed = new Vector3(0,0,-10);
    Rigidbody rb;
    float countdown;
    public override void EnterState(EnemyStateManager enemy)
    {
        rb = enemy.GetComponent<Rigidbody>();
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        rb.velocity = moveSpeed;
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
            moveSpeed *= -1;
            enemy.transform.Rotate(0, -180, 0);
        }
    }
}
