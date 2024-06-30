using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : Singleton<SceneManage>
{
    public Animator animator;
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
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelName);

    }

    public IEnumerator LoadLevel(int levelIndex)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelIndex);

    }


    public void ReseteGame()
    {
        PlayerPrefs.SetInt("UnlockedLevel", 1);
    }


    public void MenuScene()
    {
        SceneManager.LoadScene("GamePlayScene");
    }
    IEnumerator LoadLevel()
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GamePlayScene");
    }

}
