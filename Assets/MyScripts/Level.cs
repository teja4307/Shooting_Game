using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
  //  public int enemyCount;

    public EnemyHandler enemyHandler;

    //public List<Transform> enemyPositions = new List<Transform>();
    public Transform[] e_positions;
    private void Start()
    {
        enemyHandler = FindFirstObjectByType<EnemyHandler>();
        GameManager.target = e_positions.Length;
      //  int chailCount = transform.childCount;

       
            enemyHandler.NormalEnemy(enemyHandler.normal_Zomby, e_positions.Length, e_positions);
            enemyHandler._map.EnemyMapIconsInst(enemyHandler._map.world_Enemy_transform.Count);
        
    }
}
