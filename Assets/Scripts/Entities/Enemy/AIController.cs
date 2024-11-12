using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    private GameObject player;          //Player, enemy's target
    private NavMeshAgent agent;         //NavMeshAgent for AI
    private NavMeshPath path;           //Path of pathfinding which AI follows on NavMesh
    private Vector3[] pathCorners;      //Dismembered path on lines for calculation of distance
    private float fullDistance = 0f;    
    private Vector3 spawnPos;
    private Animator animator;
    private bool follows;               //if AI follows player
    public GameObject weapon;           //enemy´s weapon
    private Collider blade;
    private Patrolling patrolling;      //script for patrolling
    public EnemyData enemyData;

    private bool forwardCheck = true;

    private bool isAttacking;
    private bool knowAboutPlayer;
    private bool fallBack;

    private float buffDebuffDmg = 0;

    private float elapsedTime = 0;

    private float savedSpeedToRestore;

    public void ChangeBuffDebuffDmgBy(float value)
    {
        buffDebuffDmg += value;
    }


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = transform.GetComponent<NavMeshAgent>();
        spawnPos = agent.transform.position;
        path = new NavMeshPath();
        animator = GetComponent<Animator>();
        blade = weapon.GetComponent<Collider>();
        patrolling = GetComponent<Patrolling>();
        savedSpeedToRestore = agent.speed;

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component not found on " + gameObject.name);
        }

        if (player == null)
        {
            Debug.LogError("Player GameObject with tag 'Player' not found in the scene");
        }

        knowAboutPlayer = false;
    }

    void Update()
    {
        if (agent != null && player != null)
        {
            elapsedTime += Time.deltaTime;

            if (follows && !CanSeeTarget())
            {
                
                Vector3 direction = (player.transform.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2);
            }

            if (elapsedTime >= 0.33)
            {
                elapsedTime = 0;

                float distanceToPlayer = Vector3.Distance(agent.transform.position, player.transform.position);
                fallBack = true;
                agent.CalculatePath(player.transform.position, path);
                CalculateDistanceOfPath();

                if (knowAboutPlayer & distanceToPlayer <= 20 & path.status != NavMeshPathStatus.PathComplete)
                {
                    
                    Debug.Log(knowAboutPlayer + " " +  distanceToPlayer + " " + path.status.ToString());
                    fallBack = false;
                    patrolling.StopPatrolling();

                    Vector3 playerGroundPosition = new Vector3(player.transform.position.x, agent.transform.position.y, player.transform.position.z);
                    Debug.Log(playerGroundPosition);

                    if (distanceToPlayer <= 4 || (distanceToPlayer <= 4 && IsPlayerAbove()))
                    {
                        agent.SetDestination(playerGroundPosition);
                        follows = true;
                        AiStateAttackOrGoBack();
                    }
                    else
                    {
                        follows = true;
                        agent.SetDestination(playerGroundPosition);

                    }
                }
                else if (path.status == NavMeshPathStatus.PathComplete & fullDistance <= 20 & fullDistance != 0)
                {
                    
                    fallBack = false;
                    knowAboutPlayer = true;
                    agent.stoppingDistance = 2.5f;
                    patrolling.StopPatrolling();
                    Vector3 playerGroundPosition = new Vector3(player.transform.position.x, agent.transform.position.y, player.transform.position.z);
                    agent.SetDestination(playerGroundPosition);
                    animator.SetBool("walk", true);
                    follows = true;

                    if (fullDistance <= 4 || (fullDistance <= 4 && IsPlayerAbove()))
                    {
                        AiStateAttackOrGoBack();
                    }
                    else
                    {
                        animator.SetBool("attack", false);
                    }
                }
                else
                {
                    follows = false;
                }

                if (patrolling.hasPatrollPoints() & !follows)
                {
                    patrolling.StartPatrolling();
                    animator.SetBool("walk", true);
                }
                else if (!follows)
                {
                    agent.stoppingDistance = 0;
                    agent.SetDestination(spawnPos);
                    animator.SetBool("walk", true);
                    follows = false;

                    if (agent.remainingDistance <= 3)
                    {
                        animator.SetBool("walk", false);
                    }

                    if (knowAboutPlayer)
                    {
                        StartCoroutine(TimeUntilEnemyForgetPlayer());
                    }
                }
            }
        }
    }

    public void RestoreMovementSpeed()
    {
        agent.speed = savedSpeedToRestore;
    }

    private bool CanSeeTarget()
    {
        Vector3 toTarget = player.transform.position - transform.position;
        if (Physics.Raycast(transform.position, toTarget, out RaycastHit hit))
        {
            if (hit.transform.root == player)
                return true;
        }
        return false;
    }
    

        /// <summary>
        /// Check if player is above enemy entity
        /// </summary>
        /// <returns>
        /// bool = true/false
        /// </returns>
        private bool IsPlayerAbove()
    {
        RaycastHit hit;
        Vector3 direction = (player.transform.position - agent.transform.position).normalized;

        if (Physics.Raycast(agent.transform.position, direction, out hit))
        {
            if (hit.collider != null && hit.collider.gameObject == player)
            {
                return true;
            }
        }
        return false;
    }

    private void AiStateAttackOrGoBack()
    {
        if (follows)
        {
            animator.SetBool("walk", false);
            animator.SetBool("attack", true);
            forwardCheck = true;
        }
        else
        {
            animator.SetBool("walk", false);
            animator.SetBool("attack", false);
            forwardCheck = false;
        }
    }

        private IEnumerator TimeUntilEnemyForgetPlayer()
    {
        yield return new WaitForSecondsRealtime(5);
        //knowAboutPlayer = false;
    }

    

    private void CalculateDistanceOfPath()
    {
        if (path.status != NavMeshPathStatus.PathComplete)
        {
            fullDistance = 0f;
            return;
        }

        pathCorners = path.corners;
        var temp = 0f;
        for (int i = 1; i < pathCorners.Length; i++)
        {
            temp += Vector3.Distance(pathCorners[i - 1], pathCorners[i]);
        }
        fullDistance = temp;
    }

    public float getDamage()
    {
        return enemyData.lightAttackDamage + buffDebuffDmg;
    }


    //shows path in Scene
    private void OnDrawGizmos()
    {
        if (agent.destination != null)
        {
            Gizmos.color = Color.red;
            {
                // Draw lines joining each path corner
                pathCorners = agent.path.corners;

                for (int i = 0; i < pathCorners.Length - 1; i++)
                {
                    Gizmos.DrawLine(pathCorners[i], pathCorners[i + 1]);
                }

            }
        }
    }
}