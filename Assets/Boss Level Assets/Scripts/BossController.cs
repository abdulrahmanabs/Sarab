using StarterAssets;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private Animator _animator;
    private int _animIDHappy;
    EnemyHealth enemyHealth;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        _animIDHappy = Animator.StringToHash("PlayerDie");
    }

    private void OnEnable()
    {
        ThirdPersonController.OnPlayerDie += PlayHappyAnimation;
    }

    private void OnDisable()
    {
        ThirdPersonController.OnPlayerDie -= PlayHappyAnimation;
    }

    private void PlayHappyAnimation()
    {
        _animator.SetTrigger(_animIDHappy);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Bullet"))
        {
            Bullet Bullet = other.gameObject.GetComponent<Bullet>();
            if (Bullet.owner == ShooterWAW.player)
            {
                enemyHealth.TakeDamage(Bullet.GetBulletDamage());

                Bullet.activeHitEffect();

                Destroy(other.gameObject, 3);
            }
        }
    }

}
