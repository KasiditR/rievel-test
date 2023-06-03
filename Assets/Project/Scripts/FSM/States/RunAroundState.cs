using UnityEngine;
using UnityEngine.AI;

public class RunAroundState : State
{
    private int currentWaypointIndex = 0;
    
    public override void Enter()
    {
        textState.text = "RunAroundState";
        Debug.Log("Entering Run Around State");
        agent.isStopped = false;
        agent.speed = 15;
        agent.SetDestination(GetWaypointPosition(currentWaypointIndex));
    }
    
    public override void Update()
{
    // Logic for run around state
    
    // If the agent has reached the current waypoint, move to the next waypoint
    if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
    {
        currentWaypointIndex++;
        
        // If there are no more waypoints, transition to the attack state
        if (currentWaypointIndex >= WaypointManager.Instance.Waypoints.Length)
        {
            fsm.SetState("Chase");
            return;
        }
        
        agent.SetDestination(GetWaypointPosition(currentWaypointIndex));
    }
    
}

    
    public override void Exit()
    {
        Debug.Log("Exiting Run Around State");
        agent.isStopped = false;
        agent.speed = 5f;
    }
    
    private Vector3 GetWaypointPosition(int index)
    {
        if (index >= 0 && index < WaypointManager.Instance.Waypoints.Length)
        {
            return WaypointManager.Instance.GetWayPoint(index).position;
        }
        
        return agent.transform.position;
    }
}
