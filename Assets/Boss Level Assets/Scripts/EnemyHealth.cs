using UnityEngine;
using UnityEngine.Events;
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _bossHealth = 100f;
    [SerializeField] private Animator _animator;
    public UnityEvent onBossDeath;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void TakeDamage(float amount)
    {
        _bossHealth -= amount;

        if (_bossHealth <= 0f)
        {
            _animator.applyRootMotion = true;
            _animator.SetBool("Die", true);
            _bossHealth = 0;

            onBossDeath.Invoke();
        }
        else
        {
            _animator.SetTrigger("TakeDamage");
        }
    }


}