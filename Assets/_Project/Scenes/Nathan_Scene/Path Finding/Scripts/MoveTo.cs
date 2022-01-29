using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    public Transform[] goals;

    private UnityEngine.AI.NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        int goalSelector = Random.Range(0, 5);
        agent.destination = goals[goalSelector].position;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateNavAgentDestination();
    }

    void UpdateNavAgentDestination()
    {
        if (agent.remainingDistance < 1)
        {
            int goalSelector = Random.Range(0, 5);
            agent.destination = goals[goalSelector].position;
        }
    }
}
