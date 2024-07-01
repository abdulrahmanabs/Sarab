using System.Collections;
using UnityEngine;
using static StarterAssets.ThirdPersonController;

public class DisableAndEnableStamina : MonoBehaviour
{
    [Header("Variables and Constant")]
    public float minX = -67f;
    public float maxX = -64f;
    public float minZ = 16f;
    public float maxZ = 33f;
    public float yPosition = 0.04f;



    [Header("Components")]
    private GameObject _staminaFill;
    private GameObject _sparks;
    private GameObject _thunder;
    private GameObject _disappear;
    private void Start()
    {




        _sparks = transform.GetChild(0).GetChild(0).GetChild(2).gameObject;
        _thunder = transform.GetChild(0).GetChild(0).GetChild(3).gameObject;
        _disappear = transform.GetChild(1).gameObject;
        _staminaFill = transform.GetChild(0).gameObject;

        StartCoroutine(ShowAndHideObject());
    }
    private void OnEnable()
    {
        OnPlayerDie += DisableStaminaObjects; // الاشتراك في حدث وفاة اللاعب
    }
    private void OnDisable()
    {
        OnPlayerDie -= DisableStaminaObjects; // الاشتراك في حدث وفاة اللاعب
    }
    private void DisableStaminaObjects()
    {

        gameObject.SetActive(false);

    }

    private IEnumerator ShowAndHideObject()
    {
        while (true)
        {
            // Show object
            transform.position = GetRandomPosition();
            _staminaFill.SetActive(true);
            _sparks.SetActive(false);
            _thunder.SetActive(false);
            _disappear.SetActive(false);
            yield return new WaitForSeconds(4.5f);

            // Hide object
            _staminaFill.SetActive(false);
            _sparks.SetActive(false);
            _thunder.SetActive(false);
            _disappear.SetActive(true);
            yield return new WaitForSeconds(2f);
        }
    }

    private Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        return new Vector3(randomX, yPosition, randomZ);
    }





}
