using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent agent;
    bool x = false;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>(); // Corrected line

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
            x = agent.SetDestination(player.transform.position);
            Debug.Log(x);
            Debug.Log("Setting destination to player position: " + player.transform.position);
            Debug.Log("Current agent position: " + agent.transform.position);
            Debug.Log("Agent velocity: " + agent.velocity);

            // Additional checks
            Debug.Log("Agent path status: " + agent.pathStatus);
            Debug.Log("Agent remaining distance: " + agent.remainingDistance);
        }
    }
}
