using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;
    private NavMeshPath path;
    private Vector3[] pathCorners;
    private float fullDistance = 0f;
    private Vector3 spawnPos;
    private Animator animator;
    private bool follows;
    public GameObject weapon;
    private Collider blade;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        spawnPos = agent.transform.position;
        path = new NavMeshPath();
        animator = GetComponent<Animator>();
        blade = weapon.GetComponent<Collider>();

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
            agent.CalculatePath(player.transform.position, path);
            CalculateDistanceOfPath();
            if (path.status == NavMeshPathStatus.PathComplete & fullDistance <= 20 & fullDistance != 0)
            {
                agent.stoppingDistance = 2.5f;
                agent.SetDestination(player.transform.position);
                animator.SetBool("walk", true);
                follows = true;
                
            }
            else
            {
                agent.stoppingDistance = 0;
                agent.SetDestination(spawnPos);
                animator.SetBool("walk", true);
                follows = false;

                if (agent.remainingDistance <= 2)
                {
                    animator.SetBool("walk", false);
                    
                }
            }

            
            if (fullDistance <= 2.5f)
            {
                if (follows)
                {
                    animator.SetBool("walk", false);
                    animator.SetBool("attack", true);
                    blade.isTrigger = true;
                }
                else
                {
                    animator.SetBool("walk", false);
                    animator.SetBool("attack", false);
                    blade.isTrigger = false;
                }

            }
            else
            {
                animator.SetBool("attack", false);
                blade.isTrigger = false;
            }
            
            
            
            
        }
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
