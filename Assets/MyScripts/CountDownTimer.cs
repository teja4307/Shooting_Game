using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountDownTimer : MonoBehaviour
{
    public int startTimeInSeconds = 60; // Set the starting time in seconds
    private float currentTime;
    public Text timerText; // Make sure you assign a UI Text element in the Inspector
    private bool isCountingDown = true;

    void Start()
    {
        if(GameManager._inst!=null)
        currentTime = GameManager._inst.levelInstace.timeToComplete; // startTimeInSeconds;
        UpdateTimerText();
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        while (currentTime > 0 && isCountingDown)
        {

            if (GameManager._inst.levelCompleted)
            {
                isCountingDown = false;
                yield break; // Exits the coroutine
            }
            yield return new WaitForSeconds(1f);

           

            currentTime--;
            UpdateTimerText();
        }

        if (currentTime <= 0)
        {
            TimerFinished();
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = $"{minutes:D2}:{seconds:D2}";
    }

    void TimerFinished()
    {
        timerText.text = "Time's up!";
        GameManager._inst.GameOver();
        // Add any additional logic for when the timer finishes
    }
}
