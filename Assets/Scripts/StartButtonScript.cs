using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour
{
    public string sceneName;
    public AudioSource clickSound;
    public float delayBeforeLoad = 0.2f;

    public void OnStartButtonClicked()
    {
        if (clickSound != null)
        {
            clickSound.Play();
        }

        Invoke(nameof(LoadNextScene), delayBeforeLoad);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("LVL 1 Base Level");
    }
}