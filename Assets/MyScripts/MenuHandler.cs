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

    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Capture the current index
            buttons[i].onClick.AddListener(() => SelecteLevel(index));
        }
    }
   
    public void PlayBtnClick()
    {
        scroll_Bar.SetActive(true);
       // SceneManager.LoadScene(1);
       // print("Loading...");
    }
    
    public void SelecteLevel(int index)
    {

        //Debug.Log($"Button {index} clicked!");
        GameManager.currentLevel = index;
        SceneManager.LoadScene(1);
        //GameManager._inst.LevelEnable(index);
       
    }
}
