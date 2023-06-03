using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCharacter : BaseCharacter
{
    [SerializeField] private float healthBelow = 20;
    [SerializeField] private string stateName;
    private MonsterController monsterController;
    private bool isBelow = false;
    private void Awake()
    {
        monsterController = this.GetComponent<MonsterController>();
    }
    private void OnEnable()
    {
        this.onDie += OnDie;
        this.onHealthBelow += OnHealthBelow;
    }
    private void OnDisable()
    {
        this.onDie -= OnDie;
        this.onHealthBelow -= OnHealthBelow;
    }
    
    private void OnDie()
    {
    }
    private void OnHealthBelow()
    {
        if (this.health <= healthBelow &&!isBelow)
        {
            isBelow = true;
            monsterController.SetState(stateName);
        }
    }
}
