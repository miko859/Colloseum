using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
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

    private bool isAttacking;
    private bool knowAboutPlayer = false;
    private bool fallBack;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        spawnPos = agent.transform.position;
        path = new NavMeshPath();
        animator = GetComponent<Animator>();
        blade = weapon.GetComponent<Collider>();
        patrolling = GetComponent<Patrolling>();

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component not found on " + gameObject.name);
        }

        if (player == null)
        {
            Debug.LogError("Player GameObject with tag 'Player' not found in the scene");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (agent != null && player != null)
        {
            float distanceToPlayer = Vector3.Distance(agent.transform.position, player.transform.position);
            fallBack = true;
            agent.CalculatePath(player.transform.position, path);
            CalculateDistanceOfPath();

            if (knowAboutPlayer && distanceToPlayer <= 20 && path.status != NavMeshPathStatus.PathComplete)
            {
                fallBack = false;
                patrolling.StopPatrolling();

                Vector3 playerGroundPosition = new Vector3(player.transform.position.x, agent.transform.position.y, player.transform.position.z);

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
            else if (path.status == NavMeshPathStatus.PathComplete && fullDistance <= 20 && fullDistance != 0)
            {
                fallBack = false;
                knowAboutPlayer = true;
                agent.stoppingDistance = 2.5f;
                patrolling.StopPatrolling();
                Vector3 playerGroundPosition = new Vector3(player.transform.position.x, agent.transform.position.y, player.transform.position.z);
                agent.SetDestination(playerGroundPosition);
                animator.SetBool("walk", true);
                follows = true;

                if (fullDistance <= 3 || (fullDistance <= 3 && IsPlayerAbove()))
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
        }
        else
        {
            animator.SetBool("walk", false);
            animator.SetBool("attack", false);
        }
    }

        private IEnumerator TimeUntilEnemyForgetPlayer()
    {
        yield return new WaitForSecondsRealtime(5);
       // knowAboutPlayer = false;
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
