using UnityEngine;
using UnityEngine.UI;

public class ManageStamina : MonoBehaviour
{
    [Header("Variables and Constant")]
    [SerializeField] public Image staminaImage;
    public float currentStaminaTimer = 0f;
    [SerializeField] private float staminaMax=1;
    public bool canAttack = false;
    private void Start()
    {
        if (staminaImage != null)
        {
            staminaImage.fillAmount = currentStaminaTimer; // Stamina starts full
        }
    }
    private void OnTriggerEnter(Collider other)
    {

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            currentStaminaTimer += Time.deltaTime;
            staminaImage.fillAmount = currentStaminaTimer / staminaMax;
            if (currentStaminaTimer >= staminaMax)
            {
                canAttack = true;

                staminaImage.fillAmount = 1f; // Reset the image fill amount
            }
        }
    }
}
