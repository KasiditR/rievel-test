using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class State
{
    protected FSM fsm;
    protected NavMeshAgent agent;
    protected Transform player;
    protected Animator animator;
    
    public void SetFSM(FSM fsm, NavMeshAgent agent, Transform player, Animator animator)
    {
        this.fsm = fsm;
        this.agent = agent;
        this.player = player;
        this.animator = animator;
    }
    
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
