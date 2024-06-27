using StarterAssets;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private Animator _animator;
    private int _animIDHappy;
    MeshCollider _meshCollider;
    EnemyHealth enemyHealth;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        _animIDHappy = Animator.StringToHash("PlayerDie");
        _meshCollider = transform.GetChild(0).gameObject.GetComponent<MeshCollider>();

        
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
        _meshCollider.convex = false;
        _animator.SetTrigger(_animIDHappy);
    }
    private void OnTriggerEnter(Collider other)
    {
        print("JOINED1");
        print(other.gameObject.name);
        if (other.gameObject.CompareTag("Bullet"))
        {
            print("JOINED2");
            Bullet Bullet = other.gameObject.GetComponent<Bullet>();
            if (Bullet.owner == ShooterWAW.player)
            {
                print("JOINED3");
                enemyHealth.TakeDamage(Bullet.GetBulletDamage());

                Bullet.activeHitEffect();

                Destroy(other.gameObject, 3);
            }
        }
    }

}
