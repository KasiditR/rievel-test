using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour,IDamageable
{
    public float health;
    public float maxHealth;
    public event Action<float> onHealthChange;
    public event Action onDie;
    public event Action onHealthBelow;
    public void OnHealthChange(float value)
    {
        onHealthChange?.Invoke(value);
    }

    public void TakeDamage(float damage)
    {
        Debug.Log($"{GetType().Name} HIT");
        health -= damage;
        OnHealthChange(health);
        onHealthBelow?.Invoke();
        if (health > 0)
        {
            return;
        }

        onDie?.Invoke();
    }
}
