using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonsterController : MonoBehaviour
{
    private FSM fsm;
    [SerializeField] private UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private TMP_Text textState;
    private MonsterCharacter monsterCharacter;
    private void Awake()
    {
        fsm = new FSM();

        monsterCharacter = this.GetComponent<MonsterCharacter>();
        
        // Get the NavMeshAgent component
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        
        // Create and add the states to the FSM
        fsm.AddState("Idle", new IdleState());
        fsm.AddState("Chase", new ChaseState());
        fsm.AddState("Attack", new AttackState());
        fsm.AddState("AOE", new AOEState());
        fsm.AddState("Melee", new MeleeState());
        fsm.AddState("ShootSkill", new ShootSkillState());
        fsm.AddState("RunAroundState", new RunAroundState());
        
        // Pass the FSM, agent, player, and animator to the states
        foreach (var state in fsm.GetStates().Values)
        {
            // state.SetFSM(fsm, agent, player);
            state.SetFSM(fsm, agent, player,textState);
        }

        // Set the initial state
        fsm.SetState("Idle");
    }
    public void SetState(string state)
    {
        fsm.SetState(state);
    }
    private void Update()
    {
        // Update the FSM
        fsm.Update();
    }
}

