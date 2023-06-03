using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    
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
