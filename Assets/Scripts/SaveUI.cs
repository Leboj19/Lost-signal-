using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveUI : MonoBehaviour
{
    public TMP_Text feedbackText;   
    public Button continueButton;    

    void Start()
    {
        if (continueButton != null)
            continueButton.interactable = SaveManager.HasSave();
        if (feedbackText != null)
            feedbackText.gameObject.SetActive(false);
    }

    public void SaveGame()
    {
        bool ok = SaveManager.Save();
        if (feedbackText != null)
        {
            StopAllCoroutines();
            StartCoroutine(ShowMessage(ok ? "MISSION SAVED" : "CAN'T SAVE HERE"));
        }
    }

    public void ContinueGame()
    {
        SaveManager.Continue();
    }

    IEnumerator ShowMessage(string msg)
    {
        feedbackText.text = msg;
        feedbackText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);   
        feedbackText.gameObject.SetActive(false);
    }
}