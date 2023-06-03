using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public abstract class State
{
    protected FSM fsm;
    protected NavMeshAgent agent;
    protected Transform player;
    protected TMP_Text textState;
    public void SetFSM(FSM fsm, NavMeshAgent agent, Transform player)
    {
        this.fsm = fsm;
        this.agent = agent;
        this.player = player;
    }
    public void SetFSM(FSM fsm, NavMeshAgent agent, Transform player,TMP_Text text)
    {
        this.fsm = fsm;
        this.agent = agent;
        this.player = player;
        this.textState = text;
    }
    
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
