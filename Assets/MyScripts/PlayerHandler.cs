using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHandler : MonoBehaviour
{

    //public float moveSpeed = 10f; // Speed of the player
   // public Transform gun;
    //public Animator animCntrl;
    public GameObject _player;
    //public Camera _mainCamera;
    public GameObject healthBar;
    public static bool isPlayerDead = false;

    //public float deadZone = 0.1f; // Dead zone for touch input to avoid jitter
   // private Rigidbody rb;
   // private bool isRunning = false;
    public static Image health_img_bar;
    public static string[] color = { "#009F0A", "#8F9F00", "#A30D00" };

    public static int health = 100;
    public EnemyHandler enemyHandler;
    private void Awake()
    {
        health = 100;
        isPlayerDead = false;
    }
    private void Start()
    {
        health_img_bar = healthBar.transform.GetChild(0).GetComponent<Image>();
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            /*       GameObject zombie = other.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.
                             transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject;*/
            GameObject zombie = other.transform.root.gameObject;
            enemyHandler.enemyDictionary[zombie].ApplyDamage();
            StartCoroutine(enemyHandler.enemyDictionary[zombie].PlayerHealthBarLerp());
            enemyHandler.scratchImg.SetActive(true);
            //print(health);
            if (health <= 0)
            {
                isPlayerDead = true;
                _player.SetActive(false);
                enemyHandler.scratchImg.SetActive(false);
                health_img_bar.fillAmount = 0;
                GameManager._inst.GameOver();
            }
            // Debug.Log(zombie);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            enemyHandler.scratchImg.SetActive(false);
        }
    }
}
