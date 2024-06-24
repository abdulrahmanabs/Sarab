using System.Collections;
using UnityEngine;

public class BossSystem : MonoBehaviour
{
    [Header("Variables & Constant")]
    [SerializeField] private int _numberOfBullets = 20;
    [SerializeField] private float _spreadAngle = 45f;
    [SerializeField] private float _rotationSpeed = 100f;
    private float _lineLength = 10f;
    private bool isShooting = false;



    [Space(20)]
    [Header("Components")]



    [Space(20)]
    [Header("Referance")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPoint;



    void ShootBullets()
    {
        float angleStep = _spreadAngle / (_numberOfBullets - 1);
        float startingAngle = -_spreadAngle / 2;

        for (int i = 0; i < _numberOfBullets; i++)
        {
            float currentAngle = startingAngle + (angleStep * i);
            Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, currentAngle - 90, 0));


            GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, bulletRotation);


        }
    }
    void ShootBullets1()
    {
        StartCoroutine(SpiralShoot());
    }
    private IEnumerator SpiralShoot()
    {

        isShooting = true;
        float angleStep = 360f / _numberOfBullets;

        for (int i = 0; i < _numberOfBullets; i++)
        {
            float currentAngle = angleStep * i;
            Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, currentAngle - 90, 0));


            GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, bulletRotation);

            yield return new WaitForSeconds(0.1f);
        }

        isShooting = false;
    }
    void ShootBullets2()
    {


        float spacing = _lineLength / (_numberOfBullets - 1);

        for (int i = 0; i < _numberOfBullets; i++)
        {
            Vector3 spawnPosition = _bulletSpawnPoint.position + (_bulletSpawnPoint.right * (i * spacing - _lineLength / 2));
            GameObject bullet = Instantiate(_bulletPrefab, spawnPosition, Quaternion.Euler(_bulletSpawnPoint.rotation.x, _bulletSpawnPoint.rotation.y - 90, _bulletSpawnPoint.rotation.z));

        }


    }

}
