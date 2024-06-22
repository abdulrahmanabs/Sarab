using StarterAssets;
using System;
using UnityEngine;
using UnityEngine.UI; // To update UI elements



public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    ThirdPersonController controller;
    public Slider healthSlider; // UI element to display health
    public GameObject deathEffectPrefab;
    public event Action OnTakeDamage;
    void Start()
    {
        controller = GetComponent<ThirdPersonController>();
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
            controller.TakeDamage();
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

        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, transform.rotation);
        }

        if (controller != null)
        {
            controller.Dying();
            controller.DisableMovement();
        }

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
