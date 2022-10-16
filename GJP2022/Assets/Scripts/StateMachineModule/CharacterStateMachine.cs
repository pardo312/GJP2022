using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMachine : MonoBehaviour, IDamageable
{
    public Animator animator;

    [Header("Player Stats")]
    public CharacterResources characterResources;
    public List<Damage> damages = new List<Damage>();

    public virtual void Update()
    {
        ApplyDamages();
    }
    public void ApplyDamages()
    {
        if (damages.Count <= 0)
            return;
        for (int i = 0; i < damages.Count; i++)
            damages[i].CalculateDamage();
        List<Damage> newDamages = new List<Damage>();
        for (int i = 0; i < damages.Count; i++)
            if (!damages[i].isOver)
                newDamages.Add(damages[i]);
        damages = newDamages;
    }

    public virtual void AddDamage(Damage damageTaken)
    {
        damages.Add(damageTaken);
    }

    public virtual void TakeDamage(float amount)
    {
        //TODO reduce amount to current life: stats.life -= amount;
        //Show UI damage effect
        characterResources.health -= amount;
    }

}

