using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager _inst { get; private set; }

    public static int kills = 0;
    public  static int currentLevel = 0;
    public static int target = 0;
    public static int totalKills=0;


    public GameObject LevelCompletPanel;
    public  GameObject GameOver_Panel;
    
    public Text Kills_Txt;
    public Text currentKills_Txt;
    public Text totalKills_Txt;
   // public Text timerText;
   

    private float elapsedTime;
    
    [HideInInspector]
    public bool levelCompleted = false;
    private int hours, minutes, seconds;

    public List<GameObject> levels;

    public Level levelInstace;

    [HideInInspector]
    public bool isGameover = false;

    //private float levelTime = 0;
    public GameObject canvas_Btncontrols;

    public GameObject btns;

  static  int  justCompletedLevel = 0;

  public  static bool isRetry = false;
    private void Awake()
    {

            _inst = this;
      

        if (currentLevel == 9)
            currentLevel = 0;

        // GameInit();
        Debug.Log("Awake::" + isRetry);
      //  justCompletedLevel = PlayerPrefs.GetInt("CurrentLevel");
        if (isRetry)
        {
           
            LevelEnable(justCompletedLevel);
            currentLevel = justCompletedLevel;
            return;
        }
        LevelEnable(currentLevel);

       
       // KillsCount();
    }

    void GameInit()
    {

        _inst. LevelCompletPanel = GameObject.Find("Panel_leveComplete");
        _inst. GameOver_Panel = GameObject.Find("GameOver_Panel");
        _inst. Kills_Txt = GameObject.Find("Kills_Txt").GetComponent<Text>();
        _inst. currentKills_Txt = GameObject.Find("CurrentKills_txt").GetComponent<Text>();
        _inst. totalKills_Txt = GameObject.Find("TotalKills_txt").GetComponent<Text>();
        for (int i = 0; i < levels.Count; i++)
        {
            _inst. levels[i] = GameObject.Find("Level"+i);
        }

    }
    private void Update()
    {


       /* if (isRunning)
        {
            elapsedTime += Time.deltaTime; // Add the time passed since the last frame
            UpdateTimerText();
        }*/
    }
    public void LevelEnable(int currentLevel)
    {
       
        levels[currentLevel].SetActive(true);
        levelInstace= levels[currentLevel].GetComponent<Level>(); 
        // Level _leve= levels[currentLevel].GetComponent<Level>();
        // levelTime = _leve.timeToComplete;
        // string timeFormatted = _leve.ConvertSecondsToMinutesAndSeconds(_leve.timeToComplete); 
        // Debug.Log(timeFormatted);
    }
    public void LeveComplete()
    {
        
        //print("Working");
        kills = 0;
        target = 0;
        levelCompleted = true;
        Invoke(nameof(ShowLeveCmplet),2f);
    }

    private void ShowLeveCmplet()
    {

        // StarText();
       // canvas_Btncontrols.SetActive(false);
        LevelCompletPanel.SetActive(true);
        justCompletedLevel = currentLevel;//PlayerPrefs.GetInt("CurrentLevel");//currentLevel;
        print(isRetry + " :: " + currentLevel);
        if (isRetry==false)
        {
            currentLevel++;
            if (currentLevel > PlayerPrefs.GetInt("CurrentLevel"))
            {
                PlayerPrefs.SetInt("CurrentLevel", currentLevel);
            }
        }
        totalKills += PlayerPrefs.GetInt("TotalKills");
        PlayerPrefs.SetInt("FinalTotalKills", totalKills);
        if(totalKills_Txt != null)
        totalKills_Txt.text = "0" + totalKills;
        // int temp = PlayerPrefs.GetInt("CurrentLevel");
        /* if (currentLevel <= )
         {
             PlayerPrefs.SetInt("CurrentLevel", currentLevel);
         }*/
      //  print(currentLevel);
        // Debug.Log("Level complete");
    }

    public void GameOver()
    {
        
        if (levelCompleted)
            return;
        isGameover = true;
       // canvas_Btncontrols.SetActive(false);
        GameOver_Panel.SetActive(true);

    }

    public void Next()
    {

        //string scene = SceneManager.GetActiveScene;
        if (isRetry)
        {
            isRetry = false;
            //currentLevel = justCompletedLevel + 1;
            currentLevel++;
           if (currentLevel > PlayerPrefs.GetInt("CurrentLevel"))
            {
                PlayerPrefs.SetInt("CurrentLevel", currentLevel);
            }
        }
        SceneManager.LoadScene(1);
    }
    public void Home()
    {
       
        SceneManager.LoadScene(0);
    }
    public void Retry()
    {
        target = 0;
        kills = 0;
        isRetry = true;
       if(isGameover)
        justCompletedLevel = currentLevel;
        // currentLevel -= 1;
        // currentLevel = Mathf.Clamp(currentLevel,0,9);
        SceneManager.LoadScene(1);
    }
    
    public void KillsCount()
    {
        if (Kills_Txt != null)
        {
            Kills_Txt.text = "Kills : " + kills + "/" + target;
            currentKills_Txt.text = "0" + kills.ToString();
        }
    }
   /* void UpdateTimerText()
    {
         hours = Mathf.FloorToInt(elapsedTime / 3600F);
         minutes = Mathf.FloorToInt((elapsedTime % 3600F) / 60F);
         seconds = Mathf.FloorToInt(elapsedTime % 60F);
        timerText.text = $"{hours:00} : {minutes:00} : {seconds:00}";
    }

    public void ResetTimer()
    {
        elapsedTime = 0;
        UpdateTimerText();
    }

    public void ToggleTimer(bool start)
    {
        isRunning = start;
    }*/

    public void StarText()
    {

 /*       for(int i = 0; i < levels.Count; i++)
        {
            if (levelCompleted)
            {
                if (seconds <= _level.timeToComplete / 2 && seconds > 0)
                {
                    starText.text = "3 Star";
                }
                else if (seconds <= _level.timeToComplete && seconds > _level.timeToComplete / 2)
                {
                    starText.text = "2 Star";
                }
                else if ( seconds > _level.timeToComplete )
                {
                    starText.text = "1 Star";
                }
            }
        }*/

        /*if (levelCompleted && currentLevel == 0)
        {
            if (seconds <= 10 && seconds > 0)
            {
                starText.text = "3 Star";
            }
            if (seconds <= 20 && seconds > 10)
            {
                starText.text = "2 Star";
            }
            if (seconds <= 30 && seconds > 20)
            {
                starText.text = "1 Star";
            }
            if (seconds > 30)
            {
                starText.text = "No Star";
            }
        }

        if (levelCompleted && currentLevel == 1)
        {
            if (seconds <= 30 && seconds > 0)
            {
                starText.text = "3 Star";
            }
            if (minutes <= 40 && seconds > 30)
            {
                starText.text = "2 Star";
            }
            if (seconds <= 50 && minutes > 40)
            {
                starText.text = "1 Star";
            }
            if (seconds > 50)
            {
                starText.text = "No Star";
            }
        }

        if (levelCompleted && currentLevel == 1)
        {
            if (seconds <= 50 && seconds > 0)
            {
                starText.text = "3 Star";
            }
            if (seconds <= 1 && seconds > 50)
            {
                starText.text = "2 Star";
            }
            if (seconds <= 10 && seconds >= 1)
            {
                starText.text = "1 Star";
            }
            if (seconds > 10 && minutes >= 1)
            {
                starText.text = "No Star";
            }
        }*/
    }
}
