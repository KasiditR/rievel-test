using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour,IDamageable
{
    public float health;
    public event Action onDie;
    public event Action onHealthBelow;

    public void TakeDamage(float damage)
    {
        Debug.Log($"{GetType().Name} HIT");
        health -= damage;
        Debug.Log(health);

        onHealthBelow?.Invoke();
        if (health > 0)
        {
            return;
        }

        onDie?.Invoke();
    }
}
