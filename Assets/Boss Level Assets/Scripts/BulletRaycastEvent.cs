using System.Collections;
using UnityEngine;

public class BulletRaycastEvent : MonoBehaviour
{
    public Transform rightHandTransform;
    public float sphereRadius = 0.5f;
    public LayerMask bulletLayer;

    private Coroutine revertColorCoroutine;

    private void PerformRaycastEvent()
    {
        RaycastHit hit;
        if (Physics.SphereCast(rightHandTransform.position, sphereRadius, transform.forward, out hit, 10f, bulletLayer))
        {
            ShadowBullet bullet = hit.collider.GetComponent<ShadowBullet>();
            Debug.Log(bullet);
            if (bullet != null)
            {
                Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
                if (bulletRigidbody != null)
                {
                    // Clear the current velocity
                    bulletRigidbody.velocity = Vector3.zero;

                    // Calculate the new velocity direction (180 degrees from current forward direction)
                    Vector3 newVelocity = -bullet.transform.forward * bullet.Speed;

                    // Apply the new velocity
                    bulletRigidbody.velocity = newVelocity;
                }
                // Change the material color to white temporarily
                Renderer bulletRenderer = bullet.GetComponent<Renderer>();
                if (bulletRenderer != null)
                {
                    bulletRenderer.material.color = Color.white;

                    // Cancel any existing coroutine
                    if (revertColorCoroutine != null)
                        StopCoroutine(revertColorCoroutine);

                    // Start a coroutine to revert the color change after 1 second
                    revertColorCoroutine = StartCoroutine(RevertColorAfterDelay(bulletRenderer, 1f));
                }

                // Draw a gizmo to visualize the hit sphere
                DebugDrawHitSphere(rightHandTransform.position, sphereRadius);
            }
        }
    }

    private IEnumerator RevertColorAfterDelay(Renderer renderer, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (renderer != null)
        {
            // Revert to the original color (assuming it's not white)
            renderer.material.color = Color.white; // Change this to the original color
        }
    }

    private void DebugDrawHitSphere(Vector3 center, float radius)
    {
#if UNITY_EDITOR
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(center, radius);
#endif
    }

    // This is for editor visualization only
    private void OnDrawGizmosSelected()
    {
        DebugDrawHitSphere(rightHandTransform.position, sphereRadius);
    }
}
