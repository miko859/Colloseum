using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class AIControllerTroll : MonoBehaviour
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
    private Health health;
    private bool isAttacking = false;
    private bool knowAboutPlayer;
    private bool fallBack;
    public int heavyAttackChance = 20;


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

        knowAboutPlayer = false;
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

            if (knowAboutPlayer & distanceToPlayer <= 20 & path.status != NavMeshPathStatus.PathComplete)
            {
                fallBack = false;
                patrolling.StopPatrolling();

                Vector3 playerGroundPosition = new Vector3(player.transform.position.x, player.transform.position.z);

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
                agent.stoppingDistance = 1.5f;
                patrolling.StopPatrolling();
                Vector3 playerGroundPosition = new Vector3(player.transform.position.x, agent.transform.position.y, player.transform.position.z);
                agent.SetDestination(playerGroundPosition);
                animator.SetBool("walk", false);
                animator.SetBool("run", true);
                follows = true;

                if (fullDistance <= 5 || (fullDistance <= 5 && IsPlayerAbove()))
                {
                    AiStateAttackOrGoBack();
                }
                else
                {
                    animator.SetBool("attack1", false);
                }
            }
            else
            {
                animator.SetBool("run", false);
                follows = false;
            }

            if (patrolling.hasPatrollPoints() & !follows)
            {
                patrolling.StartPatrolling();
                animator.SetBool("walk", true);
            }
            else if (!follows)
            {
                agent.stoppingDistance = 1;
                agent.SetDestination(spawnPos);
                animator.SetBool("walk", true);
                follows = false;

                if (agent.remainingDistance <= 2)
                {
                    animator.SetBool("run", false);
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
        if (follows && !isAttacking) // Only attack if not already attacking
        {
            animator.SetBool("run", false);

            // Generate a random number between 0 and 100
            int attackChance = Random.Range(0, 100);

            if (attackChance < heavyAttackChance)
            {
                StartCoroutine(PerformAttack("attack2")); // Perform heavy attack
            }
            else
            {
                StartCoroutine(PerformAttack("attack1")); // Perform normal attack
            }
        }
        else
        {
            animator.SetBool("run", true);
        }
    }

    private IEnumerator PerformAttack(string attackType)
    {
        isAttacking = true;

        // Reset both attack bools
        animator.SetBool("attack1", false);

        animator.SetBool("attack2", false);

        animator.SetBool(attackType, true);
        float delayTime = 1.5f; // Set delay time here so the attack can finish 
        yield return new WaitForSeconds(delayTime);

        animator.SetBool("attack1", false);
        animator.SetBool("attack2", false);
        animator.SetBool(attackType, false);




        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        yield return new WaitForSeconds(stateInfo.length); // Waits for the animation to finish playing

        // Bool reset
        animator.SetBool(attackType, false);

        isAttacking = false; // Allow next attack
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
