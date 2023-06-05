using System;
using UnityEngine;

[Serializable]
public class SkillEffect
{
    public ParticleSystem particleSystem;
    // Add any additional properties for the effect, such as duration, damage over time, etc.

    public void ApplyEffect()
    {
        // Implement the logic to apply the effect here
        if (particleSystem != null)
        {
            particleSystem.Play();
        }
    }
}