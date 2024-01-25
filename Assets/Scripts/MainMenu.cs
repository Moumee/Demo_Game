using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadSceneAsync(2);
    }
    
    public void ExitButton()
    {
        Application.Quit();
    }

    public void HelpButton()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
