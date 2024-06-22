using System.Collections;
using UnityEngine;

public class BossSystem : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 20f;
    public int numberOfBullets = 20;
    public float spreadAngle = 45f;
    public float rotationSpeed = 100f;

    private bool isShooting = false;



    public float lineLength = 10f;
    // Start is called before the first frame update
    void ShootBullets()
    {
        float angleStep = spreadAngle / (numberOfBullets - 1);
        float startingAngle = -spreadAngle / 2;

        for (int i = 0; i < numberOfBullets; i++)
        {
            float currentAngle = startingAngle + (angleStep * i);
            Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, currentAngle - 90, 0));


            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletRotation);


        }
    }
    void ShootBullets2()
    {


        float spacing = lineLength / (numberOfBullets - 1);

        for (int i = 0; i < numberOfBullets; i++)
        {
            Vector3 spawnPosition = bulletSpawnPoint.position + (bulletSpawnPoint.right * (i * spacing - lineLength / 2));
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.Euler(bulletSpawnPoint.rotation.x, bulletSpawnPoint.rotation.y - 90, bulletSpawnPoint.rotation.z));

        }
    }
    void ShootBullets1()
    {
        StartCoroutine(SpiralShoot());
    }
    private IEnumerator SpiralShoot()
    {
        isShooting = true;
        float angleStep = 360f / numberOfBullets;

        for (int i = 0; i < numberOfBullets; i++)
        {
            float currentAngle = angleStep * i;
            Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, currentAngle - 90, 0));


            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletRotation);

            yield return new WaitForSeconds(0.1f); // يمكنك تعديل هذه القيمة لضبط سرعة الإطلاق
        }

        isShooting = false;
    }
}
