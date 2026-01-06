using UnityEngine;
using UnityEngine.AI;

public class AgentController2 : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform destination;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (destination != null)
        {
            agent.SetDestination(destination.position);
        }
    }
}
