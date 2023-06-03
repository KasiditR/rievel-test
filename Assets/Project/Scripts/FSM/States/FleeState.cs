using UnityEngine;
using UnityEngine.AI;

public class FleeState : State
{
    private float fleeDistance = 2;
    
    public override void Enter()
    {
        Debug.Log("Entering Flee State");
        agent.isStopped = false;
        agent.SetDestination(GetFleePosition());
    }
    
    public override void Update()
    {
        // Logic for flee state
        
        // Example: If the player moves within a certain range, transition back to attack state
        agent.SetDestination(GetFleePosition());
        if (Vector3.Distance(agent.transform.position, player.position) < fleeDistance)
        {
            fsm.SetState("Attack");
        }
    }
    
    public override void Exit()
    {
        Debug.Log("Exiting Flee State");
        agent.isStopped = true;
    }
    
    private Vector3 GetFleePosition()
    {
        // Calculate a random position away from the player
        Vector3 direction = (agent.transform.position - player.position).normalized;
        Vector3 fleePosition = agent.transform.position + direction * fleeDistance;
        NavMeshHit navMeshHit;
        
        // Find the closest valid point on the NavMesh to the calculated flee position
        if (NavMesh.SamplePosition(fleePosition, out navMeshHit, fleeDistance, NavMesh.AllAreas))
        {
            return navMeshHit.position;
        }
        
        return agent.transform.position;
    }
}
