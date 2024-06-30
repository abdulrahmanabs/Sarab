using UnityEngine;
using UnityEngine.EventSystems;

public class UISelectionSound : MonoBehaviour, ISelectHandler
{
    public AudioClip selectionSound;

    [SerializeField] AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (selectionSound != null)
        {
            audioSource.PlayOneShot(selectionSound);
        }
    }
}