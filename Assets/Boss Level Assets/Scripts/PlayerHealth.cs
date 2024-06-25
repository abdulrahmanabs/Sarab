using Microlight.MicroBar;
using StarterAssets;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


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
    [SerializeField] private Animator _animLoader;

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

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        UpdateHealthUI();

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
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
    public void PLayScene()
    {

        StartCoroutine("LoadLevel");

    }
    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(2.3f);
        _animLoader.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Boos Fight");
    }
}
