using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _currentHealth;
    [SerializeField] private Animator _animator;
    [SerializeField] private Image _bossHealthUI;
    public UnityEvent onBossDeath;
    private void Start()
    {
        _currentHealth = _maxHealth;
        _bossHealthUI.fillAmount = _currentHealth / _maxHealth;
        _animator = GetComponent<Animator>();
    }
    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        _bossHealthUI.fillAmount = _currentHealth / _maxHealth;
        if (_currentHealth <= 0f)
        {
            _animator.applyRootMotion = true;
            _animator.SetBool("Die", true);
            _currentHealth = 0;

            onBossDeath.Invoke();
        }
        else
        {
            _animator.SetTrigger("TakeDamage");
        }
    }


}