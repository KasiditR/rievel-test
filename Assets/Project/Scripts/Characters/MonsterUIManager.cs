using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterUIManager : MonoBehaviour
{
    [Header("Monster Ref")]
    [SerializeField] private MonsterCharacter _monsterCharacter;
    [Header("Health")]
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Image healthBar;

    private void Start()
    {
        // Subscribe to the events for updating the UI
        _monsterCharacter.onHealthChange += UpdateHealthUI;

        // Set initial UI values
        UpdateHealthUI(_monsterCharacter.health);
    }
    private void OnDestroy()
    {
        // Unsubscribe from the events when the script is destroyed
        _monsterCharacter.onHealthChange -= UpdateHealthUI;
    }

    private void UpdateHealthUI(float health)
    {
        float healthFillAmount = health / _monsterCharacter.maxHealth;
        healthBar.fillAmount = healthFillAmount;
        healthText.text = $"HP: {health}/{_monsterCharacter.maxHealth}";
    }
}
