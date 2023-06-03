using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    [SerializeField] private float hp = 100;
    private void Awake() 
    {
        this.health = hp;
    }
    private void OnEnable()
    {
        this.onDie += OnDie;
    }
    private void OnDisable()
    {
        this.onDie -= OnDie;
    }
    private void OnDie()
    {
        Debug.Log("Player is Die");
    }
}
