using UnityEngine;
using UnityEngine.InputSystem;

public class QuitGame : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Quit();
        }
    }

    void Quit()
    {
        Debug.Log("Quitting game...");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
