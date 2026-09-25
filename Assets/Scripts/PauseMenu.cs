using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("References")]
    public GameObject pauseMenu;      
    public GameObject settingsMenu;  
    public static bool isPaused = false;

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (settingsMenu != null && settingsMenu.activeSelf)
            {
                CloseSettings();
                return;
            }

            if (isPaused) Resume();
            else PauseGame();
        }
    }

   
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }


    public void Resume()
    {
        pauseMenu.SetActive(false);
        if (settingsMenu != null) settingsMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void OpenSettings()
    {
        if (settingsMenu != null) settingsMenu.SetActive(true);
    }


    public void CloseSettings()
    {
        if (settingsMenu != null) settingsMenu.SetActive(false);
    }

    
    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Mai Menu");
    }
}