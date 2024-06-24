using System.Collections;
using UnityEngine;

public class KnockbackEffect : MonoBehaviour
{
    public float knockbackTime = 0.2f;
    public float constForce = 5f;
    public float hitDirectionForce = 10f;
    public float inputForce = 7.5f;
    private CharacterController rb;
    private Coroutine knockbackCoroutine;
    public bool IsBeingknockbackedBack { get; private set; }
    public IEnumerator knockbackAction(Vector2 hitDirection, Vector2 constForceDirection, float inputDirection)
    {
        IsBeingknockbackedBack = true;
        Vector2 _hitForce;
        Vector2 _constForce;
        Vector2 _knockbackForce;
        Vector2 _combinedForce;
        _hitForce = hitDirection * hitDirectionForce;
        _combinedForce = constForce * constForceDirection;
        float _elapsedTime = 0f;
        while (_elapsedTime < knockbackTime)
        {
            _elapsedTime += Time.fixedDeltaTime;
            _knockbackForce = _hitForce * _combinedForce;


            if (inputDirection != 0)
            {
                _combinedForce = _knockbackForce + new Vector2(inputDirection, 0f);
            }

            else
            {
                _combinedForce = _knockbackForce;
            }
            rb.Move (_combinedForce) ;
            yield return new WaitForFixedUpdate();
        }

        IsBeingknockbackedBack = false;
    }
    public void getknockbackAction(Vector2 hitDirection, Vector2 constForceDirection, float inputDirection)
    {
        knockbackCoroutine = StartCoroutine(knockbackAction(hitDirection, constForceDirection, inputDirection));
    }
}
