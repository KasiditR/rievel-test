using UnityEngine;
using System;
public class ParticleSystemController : MonoBehaviour
{

    [SerializeField] private ParticleSystem particleSystem;
    public event Action onParticleSystemFinished;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (!particleSystem.isPlaying && !particleSystem.isEmitting)
        {
            // ParticleSystem has finished emitting particles
            if (onParticleSystemFinished != null)
            {
                onParticleSystemFinished.Invoke();
            }
        }
    }
    public void PlayEffect()
    {
        if (particleSystem != null)
        {
            particleSystem.Play();
        }
    }
}
