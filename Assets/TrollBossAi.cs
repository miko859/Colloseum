using UnityEngine;
using UnityEngine.AI;

public class TrollBossAi : MonoBehaviour
{
    public Transform[] patrolPoints; // Assign patrol points in the editor
    private int currentPatrolIndex;
    private NavMeshAgent agent;
    private Animator animator;
    public Transform player; // Assign the player's transform in the editor
    public float spottingRange = 10f;
    public float attackRange = 2f;

    private enum TrollState { Guarding, Patrolling, Chasing, Attacking }
    private TrollState currentState;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentState = TrollState.Guarding;
        currentPatrolIndex = 0;
        GoToNextPatrolPoint();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case TrollState.Guarding:
                GuardingBehavior(distanceToPlayer);
                break;
            case TrollState.Patrolling:
                PatrollingBehavior(distanceToPlayer);
                break;
            case TrollState.Chasing:
                ChasingBehavior(distanceToPlayer);
                break;
            case TrollState.Attacking:
                AttackingBehavior(distanceToPlayer);
                break;
        }
    }

    void GuardingBehavior(float distanceToPlayer)
    {
        if (distanceToPlayer < spottingRange)
        {
            currentState = TrollState.Chasing;
            animator.SetTrigger("Walk");
        }
    }

    void PatrollingBehavior(float distanceToPlayer)
    {
        if (agent.remainingDistance < 0.5f && !agent.pathPending)
        {
            GoToNextPatrolPoint();
        }

        if (distanceToPlayer < spottingRange)
        {
            currentState = TrollState.Chasing;
            animator.SetTrigger("Walk");
        }
    }

    void ChasingBehavior(float distanceToPlayer)
    {
        agent.SetDestination(player.position);

        if (distanceToPlayer < attackRange)
        {
            currentState = TrollState.Attacking;
            animator.SetTrigger("Attack1");
        }
        else if (distanceToPlayer > spottingRange)
        {
            currentState = TrollState.Patrolling;
            GoToNextPatrolPoint();
            animator.SetTrigger("Idle");
        }
    }

    void AttackingBehavior(float distanceToPlayer)
    {
        if (distanceToPlayer > attackRange)
        {
            currentState = TrollState.Chasing;
            animator.SetTrigger("Walk");
        }
        // Attack logic can be added here.
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;
        agent.destination = patrolPoints[currentPatrolIndex].position;
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;

        Debug.Log("Setting walk animation trigger");
        animator.SetTrigger("Walk");
        currentState = TrollState.Patrolling;
    }
}
