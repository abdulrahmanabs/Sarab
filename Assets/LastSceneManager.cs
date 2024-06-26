using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastSceneManager : MonoBehaviour
{
    public void OpenLink(string link) { 
    Application.OpenURL(link);
    }
    public void CloseGame()
    {
        Application.Quit();
    }
}
