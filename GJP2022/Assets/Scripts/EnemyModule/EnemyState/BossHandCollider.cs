using System;
using UnityEngine;

public class BossHandCollider : MonoBehaviour, IDamageable
{
    public Action<Collider> playerHit;
    public Action<Damage> damageBoss;

    public void AddDamage(Damage damageTaken)
    {
        damageBoss?.Invoke(damageTaken);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerHit.Invoke(other);
    }

    public void TakeDamage(float amount)
    {
        // :v
    }
}
