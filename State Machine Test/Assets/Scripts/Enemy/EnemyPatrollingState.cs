
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrollingState : EnemyBaseState
{
    private Rigidbody rb;
    private float moveSpeed = 10;

    private Vector3 fwd;
    private Ray ray;
    private RaycastHit hit;

    private float countdown;

    private NavMeshAgent agent;

    public override void EnterState(EnemyStateManager enemy)
    {
        fwd = enemy.transform.forward;
        countdown = 10;

        rb = enemy.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;

        agent = enemy.GetComponent<NavMeshAgent>();
        agent.enabled = false;
       
       
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        Debug.Log("I'm patrolling!");

        ray = new Ray(enemy.transform.position + new Vector3(0, -.5f, -5), fwd);
        if(Physics.SphereCast(ray, 5, out hit, 1, enemy.m_LayerMask))
        {
            enemy.SwitchState(enemy.chaseState);        
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
