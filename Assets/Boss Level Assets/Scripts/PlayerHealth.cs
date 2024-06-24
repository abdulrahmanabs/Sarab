using Microlight.MicroBar;
using StarterAssets;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    [Header("Variables & Constant")]
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _currentHealth;


    [Space(20)]
    [Header("Components")]
    private ThirdPersonController _TPController;



    [Space(20)]
    [Header("health Bar")]
    [SerializeField] MicroBar Playerhealthbar;


    [Space(20)]
    [Header("Referance")]
    [SerializeField] private GameObject deathEffectPrefab;


    private void Awake()
    {
        _currentHealth = _maxHealth;
    }
    void Start()
    {
        _TPController = GetComponent<ThirdPersonController>();
        InsHealthBar();
    }

    private void InsHealthBar()
    {
        Playerhealthbar.Initialize(_maxHealth);
        UpdateHealthUI();
    }

    public void TakeDamage(float amount, Vector3 attackPosition)
    {
        _currentHealth -= amount;
        UpdateHealthUI();

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }
        else
        {
            _TPController.TakeDamage(attackPosition, 5);
        }
    }


    void UpdateHealthUI()
    {
        if (Playerhealthbar != null)
        {

            Playerhealthbar.UpdateBar(_currentHealth);
        }
    }

    void Die()
    {

        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, transform.rotation);
        }

        if (_TPController != null)
        {
            _TPController.Dying();
            _TPController.DisableMovement();
        }

    }

    public void Heal(float amount)
    {
        _currentHealth += amount;
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        UpdateHealthUI();
    }
}
