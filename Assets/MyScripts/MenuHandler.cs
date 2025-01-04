using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    //public LevelSwipe _levelSwipe;
    public GameObject scroll_Bar;
    public Button[] buttons;
    public GameObject sceneLoader;
    public Slider slider;
    public Text progressText;


    private void Awake()
    {
        INIT();
    }
    void Start()
    {
       
    }

    private void INIT()
    {
      

        //PlayerPrefs.SetInt("CurrentLevel", GameManager.currentLevel);
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Capture the current index
            buttons[i].onClick.AddListener(() => SelecteLevel(index, 1));
        }
    }

    public void UnlockAllLevels()
    {
        PlayerPrefs.SetInt("CurrentLevel", 9);
        LevelLock();
    }

    public void PlayBtnClick()
    {
        scroll_Bar.SetActive(true);
        LevelLock();
        //PlayerPrefs.SetInt("CurrentLevel", GameManager.currentLevel);
      //   PlayerPrefs.GetInt("CurrentLevel");
       // SceneManager.LoadScene(1);
       // print("Loading...");
    }

    public void LevelLock()
    {
        GameManager.currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        for (int i = 0; i < buttons.Length; i++)
        {
           // if (i == 0 || i <= PlayerPrefs.GetInt("CurrentLevel"))
                if (i == 0 || i <= GameManager.currentLevel)
                {
                buttons[i].transform.GetChild(1).GetComponent<Text>().enabled = false;
                buttons[i].interactable = true;
            }
            else
            {
                buttons[i].transform.GetChild(1).GetComponent<Text>().enabled = true;
                buttons[i].interactable = false;
            }
        }
    }
    
    public void SelecteLevel(int index, int sceneIndex)
    {

        //Debug.Log($"Button {index} clicked!");
        GameManager.currentLevel = index;
        StartCoroutine(LoadAsynchronsly(sceneIndex));
        //GameManager._inst.LevelEnable(index);

    }

  /*  public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronsly(sceneIndex));
    }*/
    IEnumerator LoadAsynchronsly(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            sceneLoader.SetActive(true);
            slider.value = progress;
            progressText.text = progress * 100 + "%";
           // Debug.Log(progress);

            yield return null;
        }
    }
}
