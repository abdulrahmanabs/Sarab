using StarterAssets;
using UnityEngine;

public class PlayerBossFightController : MonoBehaviour
{
    private PlayerHealth _playerHealth;
    private ThirdPersonController _thirdPersonController;

    public delegate void BulletHitPlayerHandler(float damage);
    public static event BulletHitPlayerHandler OnBulletHitPlayer;
    private void Start()
    {

        _playerHealth = GetComponent<PlayerHealth>();
        _thirdPersonController = GetComponent<ThirdPersonController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        ShadowBullet Bullet = other.gameObject.GetComponent<ShadowBullet>();
        if (other.gameObject.CompareTag("Bullet"))
        {
            _thirdPersonController.TakeDamage(Bullet.GetBulletDamage());

            Bullet.activeHitEffect();
            Destroy(other.gameObject, 3);
        }
    }
}
