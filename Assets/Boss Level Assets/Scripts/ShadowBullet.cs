using UnityEngine;

public class ShadowBullet : MonoBehaviour
{
    [Header("Variables & Constant")]
    [SerializeField] private float _bulletSpeedShoot = 20f;
    [SerializeField] private float _bolletDamage = 10f;

    [Space(20)]
    [Header("Components")]
    private Rigidbody _bulletRB;

    [Space(20)]
    [Header("referance")]
    [SerializeField] private GameObject _hitEffectPrefab;
    void Start()
    {
        _bulletRB = GetComponent<Rigidbody>();
        _bulletRB.velocity = transform.forward * _bulletSpeedShoot;
        Destroy(gameObject, 10);
    }
    public float GetBulletSpeed()
    { return _bulletSpeedShoot; }
    void OnTriggerEnter(Collider other)
    {
        print(other.tag);
        if (other.CompareTag("Boss"))
        {
            EnemyHealth enemyHealth = other.transform.parent.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(_bolletDamage);
            }
        }
    

        // Instantiate hit effect
        if (_hitEffectPrefab != null)
        {
            GameObject hitVFX = Instantiate(_hitEffectPrefab, transform.position, transform.rotation);
            Destroy(hitVFX, 6);
        }

        // Destroy the bullet
        Destroy(gameObject);

    }
    public float GetBulletDamage(){ return _bolletDamage; }
    public void activeHitEffect()
    {
            GameObject hitVFX = Instantiate(_hitEffectPrefab, transform.position, transform.rotation);
    }
}