using UnityEngine;

public class CursorManager : MonoBehaviour
{
    void Start()
    {
        // Ensure the cursor is visible
        Cursor.visible = true;
        // Ensure the cursor is not locked
        Cursor.lockState = CursorLockMode.None;
    }
}
