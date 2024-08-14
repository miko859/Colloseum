using Unity.VisualScripting;
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
        Debug.Log("patrol start");
        agent = GetComponent<NavMeshAgent>();

        if (patrolPoints.Length > 0)
        {
            Debug.Log("enough points");
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    private void Update()
    {
        if (isPatrolling & agent.remainingDistance < 3)
        {
            Debug.Log("Next point");
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    public void StartPatrolling()
    {
        Debug.Log("start patrol");
        isPatrolling = true;
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
    }

    public void StopPatrolling()
    {
        Debug.Log("stop patrol");
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