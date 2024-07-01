using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ManageStamina : MonoBehaviour
{
    [Header("Variables and Constant")]

    public float currentStaminaTimer = 0f;
    [SerializeField] private float staminaMax = 1;
    public bool canAttack = false;
    public float delayBetweenPlays = .5f;
    [Space(20)]
    [Header("Components")]
    [SerializeField] public Image staminaImage;
    [SerializeField] private AudioClip _chargingAudioClip;
    AudioManager _audioManager;
    private GameObject _sparks;
    private GameObject _thunder;
    
    Coroutine _playSoundCoroutine;
    private void Awake()
    {
        _audioManager = AudioManager.Instance;
    }
    private void Start()
    {

        _sparks = transform.GetChild(0).GetChild(2).gameObject;
        _thunder = transform.GetChild(0).GetChild(3).gameObject;
        
        if (staminaImage != null)
        {
            staminaImage.fillAmount = currentStaminaTimer; // Stamina starts full
        }
    }
    private IEnumerator PlaySoundRepeatedly()
    {
        while (true)
        {
            // تشغيل الصوت
            _audioManager.PlaySoundEffect(_chargingAudioClip);
            // الانتظار للفترة المحددة قبل التشغيل التالي
            yield return new WaitForSeconds(_chargingAudioClip.length - 0.8f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        _playSoundCoroutine = StartCoroutine(PlaySoundRepeatedly());
        _sparks.SetActive(true);
        _thunder.SetActive(true);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            currentStaminaTimer += Time.deltaTime;
            staminaImage.fillAmount = currentStaminaTimer / staminaMax;
            if (currentStaminaTimer >= staminaMax)
            {
                staminaImage.color = Color.green;
                canAttack = true;

                staminaImage.fillAmount = 1f; // Reset the image fill amount
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(_playSoundCoroutine);
        _sparks.SetActive(false);
        _thunder.SetActive(false);
    }
    public void empty()
    {
        staminaImage.fillAmount = 0f;
    }
}
