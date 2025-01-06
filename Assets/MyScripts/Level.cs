using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Level : MonoBehaviour
{
  //  public int enemyCount;

    public EnemyHandler enemyHandler;
    public int timeToComplete;

    //public List<Transform> enemyPositions = new List<Transform>();
    //public Transform[] e_positions;
    public Transform[] normalZombiePos;
    public Transform[] mediumZombiePos;
    public Transform[] hardZombiePos;

    //public Text txt_time;
    private void Start()
    {

        enemyHandler = FindFirstObjectByType<EnemyHandler>();
        //GameManager.target = e_positions.Length;
        //  int chailCount = transform.childCount;
       /* int normal_zombie_count = normalZombiePos.Length;
        int medium_zombie_count = mediumZombiePos.Length;
        int hard_zombie_count = hardZombiePos.Length;*/

        GameManager.target = normalZombiePos.Length + mediumZombiePos.Length + hardZombiePos.Length;
        GameManager._inst.KillsCount();
        //enemyHandler.NormalEnemy(enemyHandler.normal_Zomby, e_positions.Length, e_positions);
        enemyHandler.NormalZombie(enemyHandler.normal_Zomby, normalZombiePos.Length, normalZombiePos);
        enemyHandler.MediomZomby(enemyHandler.mediom_Zomby, mediumZombiePos.Length, mediumZombiePos);
        enemyHandler.HardZomby(enemyHandler.heard_Zomby, hardZombiePos.Length, hardZombiePos);
        enemyHandler._map.EnemyMapIconsInst(enemyHandler._map.world_Enemy_transform.Count);

      //  string timeFormatted = ConvertSecondsToMinutesAndSeconds(timeToComplete); Debug.Log(timeFormatted);
       // txt_time.text = timeFormatted;
    }

 /* public  string ConvertSecondsToMinutesAndSeconds(int seconds) { 
        int minutes = seconds / 60; int remainingSeconds = seconds % 60; return $"{minutes:D2}:{remainingSeconds:D2}";
    }*/
}
