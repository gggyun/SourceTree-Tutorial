using UnityEngine;
using UnityEngine.AI;

public class MazeMonster : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
        }
    }
}
