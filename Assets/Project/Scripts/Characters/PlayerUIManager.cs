using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIManager : MonoBehaviour
{
    [Header("Player Ref")]
    [SerializeField] private PlayerCharacter playerCharacter;
    [Header("Health")]
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Image healthBar;
    [Header("Mana")]
    [SerializeField] private TMP_Text manaText;
    [SerializeField] private Image manaBar;
    [Header("Stamina")]
    [SerializeField] private TMP_Text staminaText;
    [SerializeField] private Image staminaBar;
    [Header("Exp And Money")]
    [SerializeField] private TMP_Text expText;
    [SerializeField] private TMP_Text moneyText;

    private void Start()
    {
        // Subscribe to the events for updating the UI
        playerCharacter.onHealthChange += UpdateHealthUI;
        playerCharacter.onManaChange += UpdateManaUI;
        playerCharacter.onStaminaChange += UpdateStaminaUI;
        playerCharacter.onExpChange += UpdateExpUI;
        playerCharacter.onMoneyChange += UpdateMoneyUI;

        // Set initial UI values
        UpdateHealthUI(playerCharacter.health);
        UpdateManaUI(playerCharacter.MaxMana);
        UpdateStaminaUI(playerCharacter.MaxStamina);
    }

    private void OnDestroy()
    {
        // Unsubscribe from the events when the script is destroyed
        playerCharacter.onHealthChange -= UpdateHealthUI;
        playerCharacter.onManaChange -= UpdateManaUI;
        playerCharacter.onStaminaChange -= UpdateStaminaUI;
        playerCharacter.onExpChange -= UpdateExpUI;
        playerCharacter.onMoneyChange -= UpdateMoneyUI;
    }

    private void UpdateHealthUI(float health)
    {
        // Update the health text with the new HP value
        float healthFillAmount = health / playerCharacter.maxHealth;
        healthBar.fillAmount = healthFillAmount;
        healthText.text = $"HP: {health}/{playerCharacter.maxHealth}";
    }

    private void UpdateManaUI(float mana)
    {
        // Update the mana bar fill amount with the new MP value
        float manaFillAmount = mana / playerCharacter.MaxMana;
        manaBar.fillAmount = manaFillAmount;
        manaText.text = $"MP: {mana}/{playerCharacter.MaxMana}";
    }

    private void UpdateStaminaUI(float stamina)
    {
        // Update the stamina bar fill amount with the new stamina value
        float staminaFillAmount = stamina / playerCharacter.MaxStamina;
        staminaBar.fillAmount = staminaFillAmount;
        staminaText.text = $"Stamina: {stamina}/{playerCharacter.MaxStamina}";
    }
    private void UpdateExpUI(float exp)
    {
        // Update the stamina bar fill amount with the new stamina value
        expText.text = $"EXp: {exp}";
    }
    private void UpdateMoneyUI(float money)
    {
        // Update the stamina bar fill amount with the new stamina value
        moneyText.text = $"Money: {money}";
    }
}
