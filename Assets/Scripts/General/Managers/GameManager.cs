using AYellowpaper.SerializedCollections;
using Lean.Pool;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{

    public UnityEvent OnLose;
    public UnityEvent OnLost;
    public UnityEvent OnPause;

    [SerializeField]
  SerializedDictionary <string,int> test = new SerializedDictionary <string,int> ();
    private int currentSceneCount = 0;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(int buildIndex) { }
    public void ReloadScene()
    {
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneCount++;
    }

}

   

