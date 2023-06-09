using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyBaseState
{
    float moveSpeed = 10;

    Vector3 fwd;
    Ray ray;
    RaycastHit hit;

    Transform target;
    float distance;

    NavMeshAgent agent;

    public override void EnterState(EnemyStateManager enemy)
    {
        fwd = enemy.transform.forward;

        agent = enemy.GetComponent<NavMeshAgent>();
        agent.enabled = false;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {                       
        ray = new Ray(enemy.transform.position + new Vector3(0, -0.5f, -5), fwd);
        if (Physics.SphereCast(ray, 5, out hit, 10, enemy.m_LayerMask))
        {
            target = hit.transform;
            enemy.transform.LookAt(target);
        }


        var step = moveSpeed * Time.deltaTime;
        if (target != null)
        {
            Debug.Log("I'm chasing!");
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, target.transform.position, step);
            distance = Vector3.Distance(target.position, enemy.transform.position);
            if (distance >=15)
            {
                enemy.SwitchState(enemy.returnState);
            }
        }
    }

    public override void ExitState(EnemyStateManager enemy)
    {
    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {
        
    }

    

}
