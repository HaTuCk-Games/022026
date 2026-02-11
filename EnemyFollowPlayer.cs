using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowPlayer : MonoBehaviour
{
    public Transform player;            
    public float updateInterval = 0.1f; 
    private NavMeshAgent agent;         
    private float nextUpdateTime;       


    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (player == null)
        {
            Debug.Log("Player Transform не назначен!");
        }
    }

    public void Update()
    {
        if (Time.time >= nextUpdateTime && player != null)
        {
            agent.SetDestination(player.position);
            nextUpdateTime = Time.time + updateInterval;
        }
    }
}
