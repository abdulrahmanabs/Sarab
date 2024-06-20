using StarterAssets;
using System;
using UnityEngine;
using UnityEngine.UI; // To update UI elements



public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    ThirdPersonController x;
    public Slider healthSlider; // UI element to display health
    public GameObject deathEffectPrefab;
    public event Action OnTakeDamage;
    void Start()
    {
        x = GetComponent<ThirdPersonController>();
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            x.TakeDamage();
            // Invoke the OnTakeDamage event
            //OnTakeDamage?.Invoke();
        }
    }

    void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
    }

    void Die()
    {
        // Handle player death (e.g., play animation, show game over screen, etc.)
        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, transform.rotation);
        }
        Destroy(gameObject); // Or handle respawn
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthUI();
    }
}
