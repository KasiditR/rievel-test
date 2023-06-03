using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEState : State
{
    public float radius = 5f;             // The radius of the AOE attack
    public int damageAmount = 10;         // The amount of damage to apply
    public float cooldownDuration = 5f;   // The duration of the cooldown period

    private bool isOnCooldown = false;    // Flag indicating if the skill is on cooldown
    private float cooldownTimer = 0f;     // Timer for tracking the cooldown duration
    public override void Enter()
    {
        agent.isStopped = true;
        isOnCooldown = false;
        Debug.Log("AOE");
        PerformAOEAttack();
    }

    public override void Exit()
    {
        agent.isStopped = false;
    }

    public override void Update()
    {
        // Update the cooldown timer if the skill is on cooldown
        if (isOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0f)
            {
                // Cooldown period is over, reset the flags
                isOnCooldown = false;
                cooldownTimer = 0f;
                fsm.SetState("Idle");
            }
        }
    }

    public void PerformAOEAttack()
    {
        // Check if the skill is on cooldown
        if (isOnCooldown)
        {
            Debug.Log("Skill is on cooldown.");
            return;
        }

        Collider[] colliders = Physics.OverlapSphere(agent.transform.position, radius);

        foreach (Collider collider in colliders)
        {
            IDamageable damageable  = collider.GetComponent<IDamageable>();
            damageable?.TakeDamage(damageAmount);
        }

        // Start the cooldown timer
        StartCooldownTimer();
    }
    private void StartCooldownTimer()
    {
        isOnCooldown = true;
        cooldownTimer = cooldownDuration;
    }
}
