using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private float totalPlayingTime = 0f;
    private float startTime = 0f;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        startTime = Time.time;

        print(GameManager.Instance == null);
    }

    private void Update()
    {
        float elapsedTime = Time.time - startTime;
        totalPlayingTime += elapsedTime;

        if (totalPlayingTime > 300f)
        {
            Debug.Log("lost");
        }

        startTime = Time.time;
    }
}