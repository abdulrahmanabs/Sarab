using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{

    public UnityEvent OnLose;
    public UnityEvent OnLost;
    public UnityEvent OnPause;


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

   

