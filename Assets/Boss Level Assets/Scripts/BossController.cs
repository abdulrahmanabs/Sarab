using StarterAssets;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private Animator _animator;
    private int _animIDHappy;
    MeshCollider _meshCollider;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animIDHappy = Animator.StringToHash("PlayerDie");
        _meshCollider = transform.GetChild(0).gameObject.GetComponent<MeshCollider>();
    }

    private void OnEnable()
    {
        ThirdPersonController.OnPlayerDie += PlayHappyAnimation;
    }

    private void OnDisable()
    {
        ThirdPersonController.OnPlayerDie -= PlayHappyAnimation;
    }

    private void PlayHappyAnimation()
    {
        _meshCollider.convex = false;
        _animator.SetTrigger(_animIDHappy);
    }
}
