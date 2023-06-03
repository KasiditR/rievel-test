using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeState : State
{
    private const float AttackRange = 2f;
    private bool isAttackDone;

    public override void Enter()
    {
        Debug.Log("Entering Melee State");
    }

    public override void Exit()
    {
        agent.isStopped = false;
        Debug.Log("Exiting Melee State");
    }

    public override void Update()
    {
        if (isAttackDone)
        {
            fsm.SetState("Idle");
        }
        else
        {
            // Logic for melee attack state
            agent.SetDestination(player.position);

            // Example: If the player moves out of range, transition back to idle state
            if (Vector3.Distance(agent.transform.position, player.position) < AttackRange)
            {
                agent.isStopped = true;
                animator.SetTrigger("AttackMelee"); // Trigger the melee attack animation
                MeleeAttack();
            }
        }
    }

    private void MeleeAttack()
    {
        Vector3 rayOrigin = agent.transform.position; // Starting position of the ray
        Vector3 rayDirection = agent.transform.forward; // Direction of the ray (in front of the player)
        float rayDistance = 1f; // Maximum distance to cast the ray

        RaycastHit[] hits = Physics.RaycastAll(rayOrigin, rayDirection, rayDistance);

        foreach (RaycastHit hit in hits)
        {
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            damageable?.TakeDamage(10);
        }

        isAttackDone = true;
    }
}
