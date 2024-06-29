using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSystem : MonoBehaviour
{
    [Header("Variables & Constant")]
    [SerializeField] private int _numberOfBullets = 20;
    [SerializeField] private float _spreadAngle = 45f;
    [SerializeField] private float _rotationSpeed = 100f;
    private float _lineLength = 10f;
    private bool isShooting = false;


    [Header("Boss Sound Effects")]
    public List<AudioClip> bossLought;

    public List<AudioClip> bossAttackClips;

    [Space(20)]
    [Header("Components")]
    [SerializeField] private Animator _animator;
    private AudioManager audioManager;

    [Space(20)]
    [Header("Referance")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPoint;
    private bool hasPlayedLought = false; // Boolean flag


    bool temp = true;
    private void Start()
    {
        audioManager = AudioManager.Instance;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {


        CheckIdleAnimationAndPlayLought();

    }
    void CheckIdleAnimationAndPlayLought()
    {

        // Check if the current animation state is Idle
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle Boss"))
        {
            if (temp)
            {
                temp = false;
                int randomNumber = Random.Range(1, 11);
                print(randomNumber);
                if (randomNumber > 5)
                {
                    StartCoroutine("PlayRandomBossLought");

                }
            }

        }
        else
        {
            temp = true;
        }
    }
    IEnumerator PlayRandomBossLought()
    {
        if (audioManager != null && bossLought != null)
        {
            int randomIndex = Random.Range(0, bossLought.Count);
            AudioClip LoughtClip = bossLought[randomIndex];
            audioManager.PlaySoundEffect(LoughtClip);

        }
        yield return new WaitForSeconds(1.5f);
        temp = true;
    }
    void CheckIdleAnimationAndPlayAttack()
    {
        // Check if the current animation state is Idle

        int randomNumber = Random.Range(1, 11);

        if (randomNumber > 5)
        {
            PlayRandomBossAttack();
        }

    }



    public void PlayRandomBossAttack()
    {
        if (audioManager != null && bossAttackClips != null)
        {
            int randomIndex = Random.Range(0, bossAttackClips.Count);
            AudioClip attackClip = bossAttackClips[randomIndex];
            audioManager.PlaySoundEffect(attackClip);
        }
    }

    void ShootBullets()
    {
        float angleStep = _spreadAngle / (_numberOfBullets - 1);
        float startingAngle = -_spreadAngle / 2;
        _numberOfBullets = 80;
        for (int i = 0; i < _numberOfBullets; i++)
        {
            float currentAngle = startingAngle + (angleStep * i);
            Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, currentAngle - 90, 0));

            Vector3 direction = _bulletSpawnPoint.position;
            GameObject bullet = Instantiate(_bulletPrefab, direction, bulletRotation);
            bullet.GetComponent<Bullet>().SetBulletProb(10, ShooterWAW.boss, direction);

        }
        CheckIdleAnimationAndPlayAttack();
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


            Vector3 direction = _bulletSpawnPoint.position;
            GameObject bullet = Instantiate(_bulletPrefab, direction, bulletRotation);
            bullet.GetComponent<Bullet>().SetBulletProb(10, ShooterWAW.boss, direction);
            yield return new WaitForSeconds(0.1f);
        }

        isShooting = false;
        CheckIdleAnimationAndPlayAttack();
    }
    void ShootBullets2()
    {

        CheckIdleAnimationAndPlayAttack();
        float spacing = _lineLength / (_numberOfBullets - 1);

        for (int i = 0; i < _numberOfBullets; i++)
        {
            Vector3 spawnPosition = _bulletSpawnPoint.position + (_bulletSpawnPoint.right * (i * spacing - _lineLength / 2));
            GameObject bullet = Instantiate(_bulletPrefab, spawnPosition, Quaternion.Euler(_bulletSpawnPoint.rotation.x, _bulletSpawnPoint.rotation.y - 90, _bulletSpawnPoint.rotation.z));
            Vector3 direction = spawnPosition;
            bullet.GetComponent<Bullet>().SetBulletProb(10, ShooterWAW.boss, direction);
        }


    }
    void ShootBullets3()
    {
        int numberOfBullets = 10; // عدد الرصاصات
        float angleStep = 360f / numberOfBullets; // الزاوية بين كل رصاصة والأخرى
        float bulletSpeed = 10f; // سرعة الرصاصة

        for (int i = 0; i < numberOfBullets; i++)
        {
            float currentAngle = i * angleStep;
            float radianAngle = currentAngle * Mathf.Deg2Rad;

            // حساب اتجاه إطلاق الرصاصة بناءً على الزاوية
            Vector3 direction = new Vector3(Mathf.Cos(radianAngle), 0, Mathf.Sin(radianAngle)).normalized;

            // موضع إطلاق الرصاصة
            Vector3 spawnPosition = _bulletSpawnPoint.position;

            // دوران الرصاصة بحيث تتجه للأمام
            Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, currentAngle - 90, 0));

            // إنشاء الرصاصة في الموضع المحدد والزاوية المحددة
            GameObject bullet = Instantiate(_bulletPrefab, spawnPosition, bulletRotation);

            // ضبط خصائص الرصاصة
            bullet.GetComponent<Bullet>().SetBulletProb(bulletSpeed, ShooterWAW.boss, direction);
        }

        // التحقق من حالة الانميشن وتشغيل هجوم إذا كان مطلوبًا
        CheckIdleAnimationAndPlayAttack();
    }
    void ShootBulletsWave()
    {
        // إعدادات الرصاصات
        int waveHeight = 20; // ارتفاع الموجة
        int waveWidth = 40; // عرض الموجة
        float bulletSpeed = 10f; // سرعة الرصاصة

        for (int i = 0; i < waveWidth; i++)
        {
            for (int j = 0; j < waveHeight; j++)
            {
                // حساب موضع إطلاق الرصاصة بناءً على موقع الموجة
                float xOffset = (i - waveWidth / 2) * 0.5f; // إزاحة على المحور X
                float yOffset = Mathf.Sin((float)i / waveWidth * Mathf.PI * 2) * waveHeight / 2; // إزاحة على المحور Y

                Vector3 spawnPosition = _bulletSpawnPoint.position + new Vector3(xOffset, 0, yOffset);
                spawnPosition.y = spawnPosition.y - 2f;
                Quaternion bulletRotation = Quaternion.Euler(_bulletSpawnPoint.rotation.x, _bulletSpawnPoint.rotation.y - 90, _bulletSpawnPoint.rotation.z);

                GameObject bullet = Instantiate(_bulletPrefab, spawnPosition, bulletRotation);
                Vector3 direction = _bulletSpawnPoint.forward;

                // ضبط خصائص الرصاصة
                bullet.GetComponent<Bullet>().SetBulletProb(bulletSpeed, ShooterWAW.boss, direction);
            }
        }

        // التحقق من حالة الانميشن وتشغيل هجوم إذا كان مطلوبًا
        CheckIdleAnimationAndPlayAttack();
    }
}