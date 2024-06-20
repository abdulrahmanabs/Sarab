using UnityEngine;

public class ShadowBullet : MonoBehaviour
{
    public float Speed = 20f;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private GameObject _hitEffectPrefab;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * Speed;
    }

    void OnTriggerEnter(Collider other)
    {
        print(other.tag);
        if (other.CompareTag("Boss"))
        {
            EnemyHealth enemyHealth = other.transform.parent.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(_damage);
            }
        }
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(_damage);
            }
        }

        // Instantiate hit effect
        if (_hitEffectPrefab != null)
        {
            Instantiate(_hitEffectPrefab, transform.position, transform.rotation);
        }

        // Destroy the bullet
        Destroy(gameObject);
   
    }
}