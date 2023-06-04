using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    [Header("Config Mana Point")]
    [SerializeField] private float currentMana;
    [SerializeField] private int maxStamina = 100;
    [SerializeField] private int manaRegenerationAmount = 1;

    [Header("Config Stamina")]
    [SerializeField] private float currentStamina;
    [SerializeField] private int maxMana = 100;
    [SerializeField] private int staminaRegenerationAmount = 1;
    [Header("Config Time Regenerate")]
    [SerializeField] private float regenerationInterval = 1f;
    [SerializeField] private float damageMelee;
    private bool isRegenerating;

    public float DamageMelee => damageMelee;
    private void Start()
    {
        currentStamina = maxStamina;
        currentMana = maxMana;
        StartCoroutine(RegenerateStaminaAndMana());
    }
    private IEnumerator RegenerateStaminaAndMana()
    {
        while (true)
        {
            if (currentStamina < maxStamina && !isRegenerating)
            {
                isRegenerating = true;
                yield return StartCoroutine(RegenerateStamina());
                isRegenerating = false;
            }

            if (currentMana < maxMana && !isRegenerating)
            {
                isRegenerating = true;
                yield return StartCoroutine(RegenerateMana());
                isRegenerating = false;
            }

            yield return null;
        }
    }
    private IEnumerator RegenerateStamina()
    {
        while (currentStamina < maxStamina)
        {
            currentStamina += staminaRegenerationAmount;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            yield return new WaitForSeconds(regenerationInterval);
        }
    }

    private IEnumerator RegenerateMana()
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
