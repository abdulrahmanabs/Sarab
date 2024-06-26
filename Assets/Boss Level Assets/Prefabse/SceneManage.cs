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
        StartCoroutine(LoadLevel("Boss Fight"));
    }
    public void LoadMainHubLevel()
    {
        StartCoroutine(LoadLevel("Main Hub matrix"));
    }

    public IEnumerator LoadLevel(string levelName)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelName);

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
