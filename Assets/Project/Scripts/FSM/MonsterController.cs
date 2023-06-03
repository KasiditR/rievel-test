using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private FSM fsm;
    [SerializeField] private UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem[] skillEffects;
    
    private void Start()
    {
        // Initialize the FSM
        fsm = new FSM();
        
        // Get the NavMeshAgent component
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        
        // Get the player's transform
        // player = GameObject.FindGameObjectWithTag("Player").transform;
        
        // Get the Animator component
        // animator = GetComponent<Animator>();
        
        // Create and add the states to the FSM
        fsm.AddState("Idle", new IdleState());
        fsm.AddState("Chase", new ChaseState());
        fsm.AddState("Attack", new AttackState());
        fsm.AddState("AOE", new AOEState());
        fsm.AddState("Melee", new MeleeState());
        fsm.AddState("ShootSkill", new ShootSkillState());
        
        // Set the initial state
        fsm.SetState("Idle");
        
        // Pass the FSM, agent, player, and animator to the states
        foreach (var state in fsm.GetStates().Values)
        {
            state.SetFSM(fsm, agent, player, animator);
        }
    }
    
    private void Update()
    {
        // Update the FSM
        fsm.Update();
    }
}

