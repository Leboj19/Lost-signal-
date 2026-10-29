using UnityEngine;

public class ButtonSounds : MonoBehaviour

{

    public AudioSource audioSource;
    public AudioClip clickSound;

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
}
