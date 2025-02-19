using UnityEngine;
using System;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;

    //Events
    public event Action onHealthDrained;

    public void SubstractHealth(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0) onHealthDrained?.Invoke();
    }
    public void AddHealth(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }
}
