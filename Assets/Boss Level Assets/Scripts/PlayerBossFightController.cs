using Cinemachine;
using StarterAssets;
using UnityEngine;

public class PlayerBossFightController : MonoBehaviour
{
    private PlayerHealth _playerHealth;
    private ThirdPersonController _thirdPersonController;


    public float fallThreshold = -10f; // Height limit
    public Animator playerAnimator; // Reference to the player's Animator component
    public CinemachineVirtualCamera cinemachineCamera; // Reference to the Cinemachine Virtual Camera

    private bool isDead = false;

    public delegate void BulletHitPlayerHandler(float damage);
    public static event BulletHitPlayerHandler OnBulletHitPlayer;
    private void Start()
    {

        _playerHealth = GetComponent<PlayerHealth>();
        _thirdPersonController = GetComponent<ThirdPersonController>();
        playerAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        CheckFall();
    }
    private void CheckFall()
    {
        if (transform.position.y < fallThreshold && !isDead)
        {
            Dying();
        }
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
    public void Dying()
    {
        if (isDead) return;
        _playerHealth.TakeDamage(100);
        isDead = true;

        cinemachineCamera.enabled = false;

        // Debug log for confirmation
        Debug.Log("Player has fallen and died.");
    }
    public void OnBossDeath()
    {
        Debug.Log("The boss has died!");
        _playerHealth.startloadCommingsoon();
    }
}
