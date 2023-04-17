
using UnityEngine;

public class EnemyPatrollingState : EnemyBaseState
{
    float moveSpeed = 10;
    Vector3 fwd;
    Ray ray;
    int layerMask = 1 << 3;
    Rigidbody rb;
    float countdown;
    public override void EnterState(EnemyStateManager enemy)
    {
        fwd = enemy.transform.forward;
        countdown = 5;
        rb = enemy.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        ray = new Ray(enemy.transform.position + new Vector3(0, -0.5f, 0), fwd);
        if(Physics.Raycast(ray, 10, layerMask))
        {
            Debug.Log("I see the player!");
        }

        

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
            fwd = enemy.transform.forward;
            rb.velocity = enemy.transform.forward * moveSpeed;
        }
    }
}
