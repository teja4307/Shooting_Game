using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
  //  public int enemyCount;

    public EnemyHandler enemyHandler;

    //public List<Transform> enemyPositions = new List<Transform>();
    //public Transform[] e_positions;
    public Transform[] normalZombiePos;
    public Transform[] mediumZombiePos;
    public Transform[] hardZombiePos;
    private void Start()
    {
        enemyHandler = FindFirstObjectByType<EnemyHandler>();
        //GameManager.target = e_positions.Length;
        //  int chailCount = transform.childCount;
       /* int normal_zombie_count = normalZombiePos.Length;
        int medium_zombie_count = mediumZombiePos.Length;
        int hard_zombie_count = hardZombiePos.Length;*/

        GameManager.target = normalZombiePos.Length + mediumZombiePos.Length + hardZombiePos.Length;
        //enemyHandler.NormalEnemy(enemyHandler.normal_Zomby, e_positions.Length, e_positions);
        enemyHandler.NormalZombie(enemyHandler.normal_Zomby, normalZombiePos.Length, normalZombiePos);
        enemyHandler.MediomZomby(enemyHandler.mediom_Zomby, mediumZombiePos.Length, mediumZombiePos);
        enemyHandler.HardZomby(enemyHandler.heard_Zomby, hardZombiePos.Length, hardZombiePos);
        enemyHandler._map.EnemyMapIconsInst(enemyHandler._map.world_Enemy_transform.Count);
        
    }
}
