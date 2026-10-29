using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("References")]
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public Slider volumeSlider;
    public static bool isPaused = false;

    void Start()
    {
        isPaused = false;
        Time.timeScale = 1f;

        pauseMenu.SetActive(false);
        if (settingsMenu != null) settingsMenu.SetActive(false);

        float saved = PlayerPrefs.GetFloat("MasterVolume", 1f);
        AudioListener.volume = saved;
        if (volumeSlider != null)
        {
            volumeSlider.value = saved;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.tabKey.wasPressedThisFrame)
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
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}