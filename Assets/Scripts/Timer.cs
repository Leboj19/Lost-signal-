using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timer = 60; 
    public  TextMeshProUGUI timerText;  

    private float StartTime;
    
    void Start()
    {
        StartTime = Time.time;
    }

    
    void Update()
    {
        float t= Time.time - StartTime;// THIS GIVES US THE TIME IN SECONDS SINCE THE START OF THE GAME AND "T" HOLDS THE VALUE
        string minutes = ((int)t / 60).ToString("f0");// THIS CALCULATES THE MINUTES BY DIVIDING THE TIME BY 60 AND CONVERTING IT TO AN INTEGER, THEN TO A STRING TO DISPLAY WITHOUT DECIMAL PLACES
        string seconds = (t % 60).ToString("f0");// THIS CALCULATES THE SECONDS BY TAKING THE REMAINDER OF THE TIME DIVIDED BY 60, THEN CONVERTING IT TO A STRING TO DISPLAY WITHOUT DECIMAL PLACES
        timerText.text =  seconds;// THIS UPDATES THE TEXT OF THE TIMER TO SHOW THE  SECONDS
        timer -= Time.deltaTime;


        if (timer <= 0)
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
        }

        
    }
}
