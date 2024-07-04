using UnityEngine;

public class hideButton : MonoBehaviour
{
    float time = 0;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 25)
        {
            gameObject.SetActive(false);
        }
    }
}
