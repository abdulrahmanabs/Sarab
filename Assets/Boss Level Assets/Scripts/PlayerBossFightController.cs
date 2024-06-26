using Cinemachine;
using StarterAssets;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class PlayerBossFightController : MonoBehaviour
{
    private PlayerHealth _playerHealth;
    private ThirdPersonController _thirdPersonController;

    [SerializeField] Volume postProcess;


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
        if (other.gameObject.CompareTag("Bullet"))
        {
            Bullet Bullet = other.gameObject.GetComponent<Bullet>();
            if (Bullet.owner == ShooterWAW.boss)
            {
                _thirdPersonController.TakeDamage(Bullet.GetBulletDamage());

                Bullet.activeHitEffect();
                StartCoroutine(GrayScale());
                Destroy(other.gameObject, 3);
            }
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

    IEnumerator GrayScale()
    {
        ColorAdjustments color;

        if (postProcess.profile.TryGet(out color))
        {
            float startSaturation = color.saturation.value;
            float targetSaturation = -100f;
            float duration = 2f;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);
                color.saturation.value = Mathf.Lerp(startSaturation, targetSaturation, t);
                yield return null;
            }

            // Ensure the final value is set accurately
            color.saturation.value = targetSaturation;
        }

        yield return new WaitForSeconds(2);

        if (postProcess.profile.TryGet(out color))
        {
            float startSaturation = color.saturation.value;
            float targetSaturation = 0f;
            float duration = .25f;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);
                color.saturation.value = Mathf.Lerp(startSaturation, targetSaturation, t);
                yield return null;
            }

            // Ensure the final value is set accurately
            color.saturation.value = targetSaturation;
        }
    }
}
