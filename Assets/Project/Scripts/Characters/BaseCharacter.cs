using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour,IDamageable
{
    protected float health;
    public event Action onDie;

    public void TakeDamage(float damage)
    {
        Debug.Log($"{GetType().Name} HIT");
        health -= damage;
        Debug.Log(health);
        if (health > 0)
        {
            return;
        }

        onDie?.Invoke();
    }
}
