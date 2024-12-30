using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager _inst;
    public static int kils = 0;
    public static int currentLevel = 0;
    public static int target = 0;

    public GameObject LevelCompletPanel;
    public  GameObject GameOver_Panel;
    public GameObject Menu_Panel;

    public List<GameObject> levels;

    private void Awake()
    {
        _inst = this;
        if (currentLevel == 9)
            currentLevel = 0;
        LevelEnable(currentLevel);
    }
    public void LevelEnable(int currentLevel)
    {
        levels[currentLevel].SetActive(true);
    }
    public void LeveComplete()
    {
        print("Working");
        kils = 0;
        target = 0;
        Invoke(nameof(ShowLeveCmplet),2f);
    }

    private void ShowLeveCmplet()
    {
        LevelCompletPanel.SetActive(true);
       // Debug.Log("Level complete");
    }

    public void GameOver()
    {
        GameOver_Panel.SetActive(true);
    }

    public void Next()
    {
        currentLevel++;
        //string scene = SceneManager.GetActiveScene;
        SceneManager.LoadScene(1);
    }
    public void Home()
    {
        SceneManager.LoadScene(0);
    }
    public void Retry()
    {
        target = 0;
        SceneManager.LoadScene(1);
    }



}
