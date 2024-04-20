using UnityEngine;
using UnityEngine.AI; // For navigation

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    private bool shouldChase = false; // Flag to control chasing


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.anyKey) { StartChasing(); }
        
        if(shouldChase)
        { // Only act if chasing is enabled
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            agent.SetDestination(player.position);
            
            //Looks at the player
            Vector3 lookDirection = (player.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // Adjust 5f for rotation speed
        }
    }

    // Function to start chasing when a key is pressed
    void StartChasing()
    {
        shouldChase = true;
    }
}
