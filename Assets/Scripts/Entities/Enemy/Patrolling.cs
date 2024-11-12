using UnityEngine;
using UnityEngine.AI;

public class Patrolling : MonoBehaviour
{
    public Transform[] patrolPoints;

    private bool isPatrolling = false;
    private int currentPatrolIndex = 0;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    private void Update()
    {
        if (isPatrolling & agent.remainingDistance < 3)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    public void StartPatrolling()
    {
        isPatrolling = true;
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
    }

    public void StopPatrolling()
    {
        isPatrolling = false;
        agent.ResetPath();
    }

    public bool hasPatrollPoints()
    {
        if (patrolPoints.Length > 0)
        {
            return true; 
        }
        else
        {
            return false;
        }
    }
}