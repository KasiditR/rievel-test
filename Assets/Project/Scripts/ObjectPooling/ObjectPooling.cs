using UnityEngine;
using System;

public class ObjectPooling : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 10;
    
    private Vector3 targetPosition;
    private ParticleSystem particleSystem;
    private string _tagHit;

    public void Init(float speed, int damage)
    {
        this.speed = speed;
        this.damage = damage;
    }

    public event Action onHit;
    
    public void Launch(string tagHit,Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
        this._tagHit = tagHit;
        particleSystem = this.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            particleSystem.Play();
        }
    }
    
    private void Update()
    {
        // Move the bullet towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        
        // Check if the bullet has reached the target position
        if (transform.position == targetPosition)
        {
            // Apply damage or any other desired behavior
            Debug.Log("hit the target!");
            
            // Deactivate the bullet
            gameObject.SetActive(false);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // Handle collision with other game objects
        if (other.CompareTag(_tagHit))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            damageable.TakeDamage(damage);
            
            // Deactivate the bullet
            gameObject.SetActive(false);
            onHit?.Invoke();
        }
    }
}
