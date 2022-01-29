using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    public enum ChaseState
    {
        Chasing,
        Searching,
        Lost
    }

    public Transform[] goals;
    public GameObject FieldOfView;
    public GameObject Player;
    public ChaseState CurrentChaseState = ChaseState.Lost;

    private UnityEngine.AI.NavMeshAgent agent;
    private float TargetCooldown = 100f;
    private Vector3 PlayerLastSeenPosition;

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
        if (CurrentChaseState == ChaseState.Chasing)
        {
            Debug.Log("Following Player");
            agent.destination = Player.transform.position;
            PlayerLastSeenPosition = agent.destination;
            TargetCooldown = 100f;
        }
        else if (CurrentChaseState == ChaseState.Searching)
        {
            Debug.Log("Searching last location");

            if (agent.destination != PlayerLastSeenPosition)
                agent.destination = PlayerLastSeenPosition;

            TargetCooldown -= 0.05f;

            if (TargetCooldown <= 0)
            {
                CurrentChaseState = ChaseState.Lost;
            }
        }
        else
        {
            TargetCooldown = 100f;
            CurrentChaseState = ChaseState.Lost;
            UpdateNavAgentDestination();
        }


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
