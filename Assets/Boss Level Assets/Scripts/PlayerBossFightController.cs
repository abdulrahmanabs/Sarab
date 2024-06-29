using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.Rendering;



public class PlayerBossFightController : MonoBehaviour
{
    private PlayerHealth _playerHealth;
    private ThirdPersonController _thirdPersonController;

    [SerializeField] Volume postProcess;


    public float fallThreshold = -10f; // Height limit
    public Animator playerAnimator; // Reference to the player's Animator component
    public CinemachineVirtualCamera thirdPersonCamera; // Reference to the Cinemachine Virtual Camera
    public CinemachineVirtualCamera topDownCamera;
    private bool isDead = false;

    public delegate void BulletHitPlayerHandler(float damage);
    public static event BulletHitPlayerHandler OnBulletHitPlayer;
    private Animator _animator;
    private CharacterController _characterController;
    [SerializeField] private AudioClip _fallingSound;
    private AudioManager audioManager;
    private void Start()
    {

        audioManager = AudioManager.Instance;
        _playerHealth = GetComponent<PlayerHealth>();
        _thirdPersonController = GetComponent<ThirdPersonController>();
        _characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        CheckFall();
    }
    private void CheckFall()
    {
        if (transform.position.y < fallThreshold && !isDead)
        {
            print("IS DYING");
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
                Destroy(other.gameObject, 3);
            }
        }
    }
    public void Dying()
    {
        if (isDead) return;
        _playerHealth.FallingDamage();
        isDead = true;

        thirdPersonCamera.Priority = 0;
        topDownCamera.Priority = 10;

        // Set the top-down camera to a fixed position above the player
        topDownCamera.Follow = null;
        audioManager.PlayMusic(_fallingSound);
        

        // Debug log for confirmation
        Debug.Log("Player has fallen and died.");
    }
    public void OnBossDeath()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            // Move the player to the detected ground level
            transform.position = hit.point;
        }
   
        _thirdPersonController.Grounded = true;
        _animator.SetBool("Jump", false);
        _animator.SetBool("FreeFall", false);
        _animator.SetFloat("Speed", 0);

        _thirdPersonController.DisableMovement();
        _characterController.enabled = false;











        Debug.Log("The boss has died!");
        _playerHealth.startloadCommingsoon();
    }


}
