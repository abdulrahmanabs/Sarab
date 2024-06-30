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

    [Space(20)]
    [Header("TrailRenderer Stuff")]
    private TrailRenderer tr;
    [SerializeField] Gradient playerGradient;
    [SerializeField] Gradient bossGradient;
    [SerializeField] MeshRenderer meshRenderer;
    void Awake()
    {
        _bulletRB = GetComponent<Rigidbody>();
        tr = GetComponent<TrailRenderer>();
        meshRenderer = GetComponent<MeshRenderer>();
        Destroy(gameObject, 4);
    }
    public void SetBulletProb(float damage, ShooterWAW owner, Vector3 direction, float bulletSpeed = 20f)
    {
        _bulletSpeedShoot = bulletSpeed;
        _bolletDamage = damage;
        this.owner = owner;
        _direction = direction;

        if (owner == ShooterWAW.player)
        {
            tr.colorGradient = playerGradient;
            meshRenderer.material.color = Color.white;
        }
        else
        {
            tr.colorGradient = bossGradient;
            meshRenderer.material.color = Color.black;

        }

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
            Destroy(hitVFX, 3);
        }
        if (other.gameObject.CompareTag("DeathBoundry"))
            Destroy(gameObject);

    }
    public float GetBulletDamage() { return _bolletDamage; }
    public void activeHitEffect()
    {
        Instantiate(_hitEffectPrefab, transform.position, transform.rotation);
    }


}