using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public bool IsDead => currentHealth <= 0;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

    void Die()
    {
        // Basic death hook - replace with proper death logic and network sync later
        gameObject.SetActive(false);
    }
}