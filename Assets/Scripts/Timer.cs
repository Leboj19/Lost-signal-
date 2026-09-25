using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Timer : MonoBehaviour
{
    public float timer = 300f;
    public TextMeshProUGUI timerText;

    [Header("Game Over")]
    public string startMenuSceneName = "Main Menu";
    public float gameOverDelay = 2f;

    private bool gameOver = false;

    void Start()
    {
        // StartTime = Time.time;
        timer = 300f; // THIS SETS THE TIMER TO 60 SECONDS AT THE START OF THE GAME SO IT COUNTS DOWN

        Time.timeScale = 1f;
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

        if (timer < 0)
        {
            timer = 0; // THIS IS TO MAKE SURE THE TIMER DOESNT GO PAST ZERO
            gameOver = true;
            
            timerText.text = "GAME OVER!";
            
            Time.timeScale = 0f;

            Invoke("ReturnToMainMenu", gameOverDelay);
            
            Debug.Log("Game Over!");
            
            return;

        }

        // Convert seconds into minutes and seconds
        int minutes = Mathf.FloorToInt(timer / 60f);
         int seconds = Mathf.FloorToInt(timer % 60f);
        //Display as 5:00, 4:59, 4:58,......
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        

    }
    void ReturnToMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(startMenuSceneName);
    }
}
