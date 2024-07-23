using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent agent;
    private NavMeshPath path;
    private Vector3[] pathCorners;
    private float fullDistance = 0f;
    bool x = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();       

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
            CalculateDistanceOfPath();

            agent.CalculatePath(player.transform.position, path);
            if (path.status == NavMeshPathStatus.PathComplete && fullDistance <= 20)
            {
                x = agent.SetDestination(player.transform.position);
            }
            Debug.Log(fullDistance);
            /*
            Debug.Log(x);
            Debug.Log("Setting destination to player position: " + player.transform.position);
            Debug.Log("Current agent position: " + agent.transform.position);
            Debug.Log("Agent velocity: " + agent.velocity);

            // Additional checks
            Debug.Log("Agent path status: " + agent.pathStatus);
            Debug.Log("Agent remaining distance: " + agent.remainingDistance);*/
            OnDrawGizmos();
            
        }
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

    private void CalculateDistanceOfPath()
    {
        fullDistance = 0f;
        int i;
        for (i = 1; i < pathCorners.Length; i++)
        {
            fullDistance += Vector3.Distance(pathCorners[i - 1], pathCorners[i]);
        }
    }
}
