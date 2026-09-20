using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OnStartGameClick()
    {
        SceneManager.LoadScene("Narration");
    }

    public void OnContinueClick()
    {
        SceneManager.LoadScene("LVL 1 Base Level");
    }

    public void OnQuitGameClick()

    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}