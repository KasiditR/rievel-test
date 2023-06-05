using System;
using UnityEngine;
public enum SkillType
{
    Heal,
    Slash,
    AOE
}

[Serializable]
public class Skill
{
    public string name;
    public float value;
    public float coolDown;
    public int resourceCost;
    public SkillType skillType;
    public SkillEffect effect;

    private float lastUsedTime;

    public bool CanUse()
    {
        Debug.Log(Time.time - lastUsedTime);
        Debug.Log(coolDown);
        return Time.time - lastUsedTime >= coolDown;
    }

    public void Use()
    {
        lastUsedTime = Time.time;
        // Perform actions based on the skill, such as dealing damage or activating abilities
        Debug.Log("Using skill: " + name);
        Debug.Log("Damage: " + value);

        if (effect != null)
        {
            effect.ApplyEffect();
        }
    }
}