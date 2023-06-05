using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkillSystem : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _playerCharacter;
    [SerializeField] private SkillContainer _skillContainer;
    private Skill[] _skills;

    public Skill[] Skills { get => _skills;}

    // Use this event to notify other scripts when a skill is used
    public event Action<Skill> OnSkillUsed;

    public void InitSkill(Skill[] skills)
    {
        this._skills = skills;
        this._skillContainer.Init(this);
    }
    public void UseSkill(Skill skill)
    {
        if (skill.CanUse())
        {
            // Check if the player has enough MP to use the skill
            if (HasEnoughResources(skill.resourceCost))
            {
                ConsumeResources(skill.resourceCost);
                skill.Use();

                // Raise the OnSkillUsed event to notify other scripts
                OnSkillUsed?.Invoke(skill);
            }
            else
            {
                Debug.Log("Not enough MP to use the skill!");
            }
        }
        else
        {
            Debug.Log("Skill is on cooldown!");
        }
    }

    private bool HasEnoughResources(int amount)
    {
        // Check if player has enough MP
        return _playerCharacter.GetManaPoint() >= amount;
    }

    private void ConsumeResources(int amount)
    {
        // Consume MP
        _playerCharacter.DecreaseManaPoint(amount);
    }
}
