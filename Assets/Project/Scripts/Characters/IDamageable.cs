using System;

public interface IDamageable
{
    public event Action onHealthBelow;
    public event Action onDie;
    public void TakeDamage(float damage);
}