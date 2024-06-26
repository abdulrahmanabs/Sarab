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
    public void BossFightScene()
    {
        SceneManager.LoadScene("Boos Fight");
    }
    public void MainHubScene()
    {
        SceneManager.LoadScene("Main Hub matrix");
    }
    public void CommingSoonScene()
    {
        SceneManager.LoadScene("Comming soon");
    }
}
