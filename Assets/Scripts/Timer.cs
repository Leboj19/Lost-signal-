using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    [Header("Timer settings")]
    public float timer = 300f;
    public float startingTime = 300f;
    public TextMeshProUGUI timerText;


    [Header("Robot Battery HUD")]
    public Image batteryFill;
    public TextMeshProUGUI batteryPercentageText;

    public Color fullBatteryColor = new Color32(112, 214, 255, 255);
    public Color mediumBatteryColor = Color.yellow;
    public Color lowBatteryColor = Color.red;

    
    [Header("Emergency Power Warning")]
    public TextMeshProUGUI powerWarningText;

    private bool powerWarningShown = false;


 [Header("Game Over")]
    public string startMenuSceneName = "Main Menu";
    public float gameOverDelay = 2f;

    
    [Header("Victory Screen")]
    public GameObject victoryPanel;
    public float victoryScreenDelay = 5f;



    private bool gameOver = false;


    void Start()
    {
        // StartTime = Time.time;
        timer = startingTime; // THIS SETS THE TIMER TO 60 SECONDS AT THE START OF THE GAME SO IT COUNTS DOWN

        Time.timeScale = 1f;

        if(victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }

        if (powerWarningText != null)
        {
            powerWarningText.gameObject.SetActive(false);
        }

        UpdateBattery();
        UpdateTimerText();

    }
    void Update()
    {
        if (gameOver)
            return;

        //float t= Time.time - StartTime;// THIS GIVES US THE TIME IN SECONDS SINCE THE START OF THE GAME AND "T" HOLDS THE VALUE
        //string minutes = ((int)t / 60).ToString("f0");// THIS CALCULATES THE MINUTES BY DIVIDING THE TIME BY 60 AND CONVERTING IT TO AN INTEGER, THEN TO A STRING TO DISPLAY WITHOUT DECIMAL PLACES
        // string seconds = (t % 60).ToString("f0");// THIS CALCULATES THE SECONDS BY TAKING THE REMAINDER OF THE TIME DIVIDED BY 60, THEN CONVERTING IT TO A STRING TO DISPLAY WITHOUT DECIMAL PLACES
        //timerText.text =  seconds;// THIS UPDATES THE TEXT OF THE TIMER TO SHOW THE  SECONDS
        timer -= Time.deltaTime;
        /* if (timer <= 0)
                {
                    Debug.Log("Game Over!");
                    timerText.text = "Game Over!";
        }
                if (timer <= 0)
                {
                    timer = 0; // THIS IS TO MAKE SURE THE TIMER DOESNT GO PAST ZERO
                }
                if (timer < 20)
                {
                    timerText.color= Color.red;
                }*/
        if (timer <= 240f && !powerWarningShown)
        {
            powerWarningShown = true;

            if (powerWarningText != null)
            {
                powerWarningText.text =
                    "POWER WARNING!\n" +
                    "Robot battery reserves are dropping.\n" +
                    "Find the emergency power computer and " +
                    "repair its wiring to gain +30 seconds.";

                powerWarningText.gameObject.SetActive(true);

                StartCoroutine(HidePowerWarning());
            }
        }

        if (timer < 0f)
        {
            timer = 0f; // THIS IS TO MAKE SURE THE TIMER DOESNT GO PAST ZERO
            gameOver = true;

            if (timerText != null)
                timerText.text = "GAME OVER!";

            UpdateBattery();

            Debug.Log("Game Over!");

            StartCoroutine(GameOverSequence());

            return;

        

        }
        UpdateTimerText();
        UpdateBattery();






    }
    void UpdateTimerText()
    {
        if (timerText == null)
            return;


        // Convert seconds into minutes and seconds
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }
    public void AddTime(float seconds)
    {
        if (gameOver || seconds <= 0f)
            return;

        timer += seconds;

        UpdateTimerText();
        UpdateBattery();

        Debug.Log("Robot power restored! Added " +
        seconds + " seconds. Time remaining: " + timer);
    }
    void UpdateBattery()
    {
        float battery = Mathf.Clamp01(timer / startingTime);

        if (batteryFill != null)
        {
            batteryFill.fillAmount = battery;

            if (battery <= 0.2f)
                batteryFill.color = lowBatteryColor;
            else if (battery <= 0.5f)
                batteryFill.color = mediumBatteryColor;
            else
                batteryFill.color = fullBatteryColor;
        }
        if (batteryPercentageText != null)
        {
            int percentage = Mathf.CeilToInt(battery * 100f);
            batteryPercentageText.text = percentage + "%";
        }
    }

    public void GameCompleted()
    {
        if (gameOver)
            return;

        gameOver = true;

        FPController player = FindFirstObjectByType<FPController>();

        if (player != null)
        {
            player.StopPlayer();
        }
        else
        {
            Debug.LogWarning("  FPController not found!");
        }
       
        if (timerText != null)
        {
            timerText.text = "MISSION COMPLETE!";
        }

        if (powerWarningText != null)
        {
            powerWarningText.gameObject.SetActive(false); 
        }

        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }
        else
        {
            Debug.Log("ALL OBJECTIVES COMPLETED - GAME FINISHED!");
        }

        Time.timeScale = 0f;

      StartCoroutine(ReturnToMainMenu());
    }
    private IEnumerator GameOverSequence()
    {
        yield return new WaitForSecondsRealtime(gameOverDelay);

        Time.timeScale = 1f;

        SceneManager.LoadScene("Main Menu");
    }
    private IEnumerator ReturnToMainMenu()
    {
        Time.timeScale = 0f;
        
        yield return new WaitForSecondsRealtime(victoryScreenDelay);

        Time.timeScale = 1f;

        SceneManager.LoadScene("Main Menu");
    }
    private IEnumerator HidePowerWarning()
    {
        yield return new WaitForSecondsRealtime(7f);

        if (powerWarningText != null)
        {
            powerWarningText.gameObject.SetActive(false);
        }
    }
}
