using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
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

}
