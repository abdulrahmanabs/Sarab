using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private bool _isTakingDamage = false;
    private PlayerHealth _playerHealth;

    private static readonly int TakeDamageTrigger = Animator.StringToHash("TakeDamageTrigger");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerHealth = GetComponent<PlayerHealth>();

        // Subscribe to the OnTakeDamage event
        _playerHealth.OnTakeDamage += TriggerTakeDamageAnimation;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the OnTakeDamage event to avoid memory leaks
        _playerHealth.OnTakeDamage -= TriggerTakeDamageAnimation;
    }

    private void TriggerTakeDamageAnimation()
    {
        if (!_isTakingDamage)
        {
            _isTakingDamage = true;
            _animator.SetTrigger(TakeDamageTrigger);
            StartCoroutine(ResetTakingDamage());
        }
    }

    private IEnumerator ResetTakingDamage()
    {
        yield return new WaitForSeconds(1f); // Adjust the duration based on your animation length
        _isTakingDamage = false;
    }
}
