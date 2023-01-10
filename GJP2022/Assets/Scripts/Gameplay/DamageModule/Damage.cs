using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    public IDamageable target;
    public DamageTypes damageType;
    public bool isOver;
    public float amount;
    public virtual void CalculateDamage()
    {
        
    }
}

public enum DamageTypes{
    normal,
    electric,
    poison
}

public class NormalDamage : Damage
{
    public NormalDamage()
    {

    }
    public override void CalculateDamage()
    {
        base.CalculateDamage();
        target.TakeDamage(amount);
        isOver = true;
    }
}

public class FireDamage : Damage
{
    float lastDamageTime = 0;
    float timeBetweenDamage = 1;
    int numberOfDamage = 10;

    public FireDamage()
    {
        lastDamageTime = Time.time + timeBetweenDamage;
        amount *= 2;
    }

    public override void CalculateDamage()
    {
        if (isOver)
            return;
        base.CalculateDamage();
        if (lastDamageTime - Time.time < 0)
        {
            float damage = Mathf.Ceil(amount / 2);
            target.TakeDamage(damage);
            amount = damage;
            numberOfDamage -= 1;
        }
        if (numberOfDamage < 0)
            isOver = true;
    }
}
