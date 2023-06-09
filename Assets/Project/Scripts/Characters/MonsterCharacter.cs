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
        this.health = this.maxHealth;
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
        PlayerCharacter player = GameObject.FindObjectOfType<PlayerCharacter>();
        player.SetExp(100);
        player.SetMoney(100);
        Destroy(this.gameObject);
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
