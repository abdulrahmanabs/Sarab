using UnityEngine;

public class BeaconController : MonoBehaviour
{
    public GameObject torch1;
    public GameObject torch2;
    public Light torchLight1;
    public Light torchLight2;
    public bool isBeaconLit = false;
    public float rotationSpeed = 10f; // سرعة الدوران

    void Start()
    {
        UpdateTorchState();
    }

    void Update()
    {
        // يمكن هنا إضافة أي شروط أو أحداث لتشغيل وإطفاء الشعلة
        if (Input.GetKeyDown(KeyCode.L)) // مثال: اضغط المفتاح L لتشغيل وإطفاء الشعلة
        {
            ToggleBeacon();
        }

        // تدوير المصابيح إذا كانت الشعلة مشتعلة
        if (isBeaconLit)
        {
            RotateTorches();
        }
    }

    public void ToggleBeacon()
    {
        isBeaconLit = !isBeaconLit;
        UpdateTorchState();
    }

    private void UpdateTorchState()
    {
        if (torch1 != null && torch2 != null)
        {
            torch1.SetActive(isBeaconLit);
            torch2.SetActive(isBeaconLit);
        }

        if (torchLight1 != null && torchLight2 != null)
        {
            torchLight1.enabled = isBeaconLit;
            torchLight2.enabled = isBeaconLit;
        }
    }

    private void RotateTorches()
    {
        if (torch1 != null)
        {
            torch1.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
        if (torch2 != null)
        {
            torch2.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
