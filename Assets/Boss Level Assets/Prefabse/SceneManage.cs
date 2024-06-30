using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : Singleton<SceneManage>
{
    public Animator animator;
    private int currentLevel=1;

    private void OnEnable()
    {
        currentLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        
    }
    public void CloseApp()
    {
        print("Exit");
        Application.Quit();
    }
    public void PLayScene()
    {
        StartCoroutine("LoadLevel");

    }

    public void LoadBossFightLevel()
    {

        StartCoroutine(LoadLevel(2));
        PlayerPrefs.SetInt("UnlockedLevel", 2);
    }
    public void PlayButton()
    {
        if (PlayerPrefs.GetInt("UnlockedLevel", 1) == 1)
            StartCoroutine(LoadLevel("Main Hub matrix"));
        else
            LoadBossFightLevel();
    }

    public IEnumerator LoadLevel(string levelName)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene(levelName);

    }

    public IEnumerator LoadLevel(int levelIndex)
    {
        print("1");
        animator.SetTrigger("Start");
        print("2");
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene(levelIndex);
        print("44");
    }


    public void ReseteGame()
    {
        currentLevel = 1;
        PlayerPrefs.SetInt("UnlockedLevel", currentLevel);
    }


    public void MenuScene()
    {
        SceneManager.LoadScene("GamePlayScene");
    }
    IEnumerator LoadLevel()
    {
        animator.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene("GamePlayScene");
    }

    public void ReloadLevel() {

        print("JOINED");
        print(SceneManager.GetActiveScene().buildIndex);
        int level = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadLevel(level));
    }

}
