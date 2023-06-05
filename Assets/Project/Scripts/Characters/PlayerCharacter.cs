using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    [Header("Config Mana Point")]
    [SerializeField] private float currentMana;
    [SerializeField] private int maxStamina = 100;
    [SerializeField] private float manaRegenerationAmount = 1;

    [Header("Config Stamina")]
    [SerializeField] private float currentStamina;
    [SerializeField] private int maxMana = 100;
    [SerializeField] private float staminaRegenerationAmount = 1;

    [Header("Config Time Regenerate")]
    [SerializeField] private float regenerationInterval = 1f;
    [SerializeField] private float damageMelee;

    [Header("Config Skill")]
    [SerializeField] private SkillSystem skillSystem;
    [SerializeField] private Skill[] skills;

    [SerializeField] private Transform point;

    private bool isRegenerating;
    private bool isRegenerateStamina;

    public float DamageMelee => damageMelee;
    private void Start()
    {
        currentStamina = maxStamina;
        currentMana = maxMana;
        StartCoroutine(RegenerateStamina());
        StartCoroutine(RegenerateMana());

        skillSystem.InitSkill(skills);
        skillSystem.OnSkillUsed += OnSkill;
    }

    private void OnSkill(Skill skill)
    {
        switch (skill.skillType)
        {
            case SkillType.Heal:
                this.health += skill.value;
                break;
            case SkillType.AOE:
                AOEAttack(skill.value);
                break;
            case SkillType.Slash:
                SlashAttack(skill.value);
                break;
            default:
                break;
        }
        
    }

    private void SlashAttack(float damage)
    {
        // Get a projectile from the object pool
        GameObject projectileRef = Resources.Load("Slash") as GameObject; 
        GameObject projectile = Instantiate(projectileRef,Vector3.zero,Quaternion.identity);
        

        // Set the projectile's position and rotation to match the shooter's
        projectile.transform.position = point.position;
        projectile.transform.rotation = point.rotation;

        // Activate the projectile
        projectile.SetActive(true);

        Vector3 target = this.point.forward * 20;
        ObjectPooling objectPooling  = projectile.GetComponent<ObjectPooling>();
        objectPooling.onHit += () => Destroy(projectile);
        objectPooling.Launch("Monster",target);
    }

    private void AOEAttack(float damage)
    {
        LayerMask monsterLayer = LayerMask.NameToLayer("Monster");
        Collider[] hitMonsters = Physics.OverlapSphere(this.transform.position,5,monsterLayer);
        foreach (Collider hit in hitMonsters)
        {
            IDamageable damageable = hit.GetComponent<IDamageable>();
            damageable?.TakeDamage(DamageMelee);
        }
    }

    private IEnumerator RegenerateStamina()
    {
        while (true)
        {
            if (currentStamina < maxStamina && !isRegenerating)
            {
                isRegenerating = true;
                yield return StartCoroutine(IncreaseStamina());
                isRegenerating = false;
            }
            yield return null;
        }
    }

    private IEnumerator RegenerateMana()
    {
        while (true)
        {

            if (currentMana < maxMana && !isRegenerating)
            {
                isRegenerating = true;
                yield return StartCoroutine(IncreaseMana());
                isRegenerating = false;
            }

            yield return null;
        }
    }

    private IEnumerator IncreaseStamina()
    {
        while (currentStamina < maxStamina)
        {
            currentStamina += staminaRegenerationAmount;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            yield return new WaitForSeconds(regenerationInterval);
        }
    }

    private IEnumerator IncreaseMana()
    {
        while (currentMana < maxMana)
        {
            currentMana += manaRegenerationAmount;
            currentMana = Mathf.Clamp(currentMana, 0, maxMana);
            yield return new WaitForSeconds(regenerationInterval);
        }
    }

    public float GetManaPoint()
    {
        return currentMana;
    }

    public void DecreaseManaPoint(float value)
    {
        currentMana -= value;
    }

    public float GetStamina()
    {
        return currentStamina;
    }

    public void DecreaseStamina(float value)
    {
        currentStamina -= value;
    }
    private IEnumerator CoroutineRegenerateStamina()
    {
        
        yield return null;
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
