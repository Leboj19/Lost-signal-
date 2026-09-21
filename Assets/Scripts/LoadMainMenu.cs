using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningScreen : MonoBehaviour
{
    public float displayTime = 5f;
    void Start()
    {
        Invoke(nameof(GoToMainMenu), displayTime);
    }

    void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}