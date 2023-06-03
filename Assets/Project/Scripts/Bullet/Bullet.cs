using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    
    private Vector3 targetPosition;
    private ParticleSystem particleSystem;
    public void Launch(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
        particleSystem = this.GetComponent<ParticleSystem>();
        particleSystem?.Play();
    }
    
    private void Update()
    {
        // Move the bullet towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        
        // Check if the bullet has reached the target position
        if (transform.position == targetPosition)
        {
            // Apply damage or any other desired behavior
            Debug.Log("Bullet hit the target!");
            
            // Deactivate the bullet
            gameObject.SetActive(false);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        // Handle collision with other game objects
        if (other.CompareTag("Player"))
        {
            // Apply damage or any other desired behavior
            Debug.Log("Bullet hit an enemy!");
            IDamageable damageable = other.GetComponent<IDamageable>();
            damageable.TakeDamage(damage);
            
            // Deactivate the bullet
            gameObject.SetActive(false);
        }
    }
}
