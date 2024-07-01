using StarterAssets;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private Animator _animator;
    private int _animIDHappy;
    MeshCollider _meshCollider;
    EnemyHealth enemyHealth;
    [SerializeField] private AudioClip bossTakeDamageClip;
    private AudioManager audioManager;
    private void Awake()
    {
        audioManager = AudioManager.Instance;
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
    {   get
        _animator.SetTrigger(_animIDHappy);
    }
    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("Bullet"))
        {

            Bullet Bullet = other.gameObject.GetComponent<Bullet>();
            if (Bullet.owner == ShooterWAW.player)
            {
                audioManager.PlaySoundEffect(bossTakeDamageClip);
                enemyHealth.TakeDamage(Bullet.GetBulletDamage());

                Bullet.activeHitEffect();

                Destroy(other.gameObject);
            }
        }
    }

}
