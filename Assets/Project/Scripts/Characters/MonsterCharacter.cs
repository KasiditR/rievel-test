using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCharacter : BaseCharacter
{
    [SerializeField] private float hp;
    [SerializeField] private Animator anim;
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
        anim.SetTrigger("isDie");
    }
}
