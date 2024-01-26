using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    PlayerControls playerControls;
    public GameObject pausePanel;

    private void Awake()
    {
        playerControls = FindFirstObjectByType<PlayerMovement>().playerControls;
    }
    public void ExitButton()
    {
        Application.Quit();
    }

    public void ResumeButton()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void HomeButton()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void DoPause(InputAction.CallbackContext context)
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }


}
