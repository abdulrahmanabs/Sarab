using UnityEngine;


public enum ShooterWAW { player, boss };
public class Bullet : MonoBehaviour
{
    [Header("Variables & Constant")]
    [SerializeField] private float _bulletSpeedShoot = 20f;
    [SerializeField] private float _bolletDamage = 10f;
    private Vector3 _direction;
    public ShooterWAW owner;
    [Space(20)]
    [Header("Components")]
    private Rigidbody _bulletRB;

    [Space(20)]
    [Header("referance")]
    [SerializeField] private GameObject _hitEffectPrefab;
    void Start()
    {
        _bulletRB = GetComponent<Rigidbody>();

        Destroy(gameObject, 10);
    }
    public void SetBulletProb(float damage, ShooterWAW owner, Vector3 direction)
    {
        _bolletDamage = damage;
        this.owner = owner;
        _direction = direction;
    }
    void Update()
    {
        _bulletRB.velocity = transform.forward * _bulletSpeedShoot;
    }

    public float GetBulletSpeed()
    { return _bulletSpeedShoot; }
    void OnTriggerEnter(Collider other)
    {
        // Instantiate hit effect
        if (_hitEffectPrefab != null)
        {
            GameObject hitVFX = Instantiate(_hitEffectPrefab, transform.position, transform.rotation);
            Destroy(hitVFX, 6);
        }

        // Destroy the bullet
        Destroy(gameObject);

    }
    public float GetBulletDamage() { return _bolletDamage; }
    public void activeHitEffect()
    {
        GameObject hitVFX = Instantiate(_hitEffectPrefab, transform.position, transform.rotation);
    }
}