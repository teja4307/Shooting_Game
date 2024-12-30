using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEditor;

public class EnemyHandler : MonoBehaviour
{
    //public PlayerHandler playerHandler;
    public GameManager gameManager;
    public Transform plyer;
    public GameObject normal_Zomby;
    public GameObject mediom_Zomby;
    public GameObject heard_Zomby;
    public GameObject scratchImg;

    public Transform[] normalZombiPositons;
    public Transform[] mediumZombiePos;
    public Transform[] hardZombiePos;

    public int normal_zomby_count = 5;
    public int medium_zomby_count = 4;
    public int heard_zomby_count = 3;


    [SerializeField] public Map _map;

    // Dictionary to store enemy GameObject and corresponding EnemeyData
    public Dictionary<GameObject, EnemyData> enemyDictionary = new Dictionary<GameObject, EnemyData>();

    


    
    // EnemeyData e_one_data;
    private void Awake()
    {
      
        // Instantiate the enemy and set up its data
        /* for(int i=0;i<enemy.Length; i++)
         {
             GameObject _enemy = Instantiate(enemy[i].gameObject, tempEtrns[i].position, Quaternion.identity);
              _enemy.name = "Zombie_"+i;

             EnemeyData e_one_data = new EnemeyData(_enemy.name, 100.0f, plyer);

             // Add the enemy GameObject and its data to the dictionary
             enemyDictionary.Add(_enemy, e_one_data);
         }*/

      //  NormalEnemy(normal_Zomby, normal_zomby_count, normalZombiPositons);
      //  MediomZomby(mediom_Zomby, medium_zomby_count, mediumZombiePos);
       // HeardZomby(heard_Zomby, heard_zomby_count, hardZombiePos);
     //  _map.EnemyMapIconsInst(_map.world_Enemy_transform.Count);
    }

    public void NormalEnemy(GameObject normalEnemy, int count, Transform[] _trns)
    {
      
        ZomboyInstanciation(normalEnemy, count, _trns, EnemyData.ZombieType.Normal);
    }
    private void MediomZomby(GameObject mediomZombie, int count, Transform[] _trns)
    {
        ZomboyInstanciation(mediomZombie, count, _trns, EnemyData.ZombieType.Medium);
    }
    private void HeardZomby(GameObject heardZombie, int count, Transform[] _trns)
    {
        ZomboyInstanciation(heardZombie, count, _trns, EnemyData.ZombieType.Hard);
    }

    private void ZomboyInstanciation(GameObject Zombie, int count, Transform[] _trns, EnemyData.ZombieType zombieType)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject _enemy = Instantiate(Zombie, _trns[i].position, Quaternion.identity);
            _enemy.name = zombieType + "Zombie_" + i;
            //_enemy.GetComponent<NavMeshAgent>().SetDestination(plyer.position);
            EnemyData e_one_data = new EnemyData(_enemy.name,
                100.0f,
                plyer,
                _enemy.GetComponent<NavMeshAgent>(),
                _enemy.transform.GetChild(0).GetComponent<Animator>(), zombieType);

            // Add the enemy GameObject and its data to the dictionary
            enemyDictionary.Add(_enemy, e_one_data);
            _map.world_Enemy_transform.Add(_enemy.transform);

        }
    }

    private void Update()
    {
        // Iterate through the dictionary and update each enemy's destination
        EnemyMovementAndAttack();
    }

    private void EnemyMovementAndAttack()
    {
        foreach (var pair in enemyDictionary)
        {
            EnemyData data = pair.Value;

            // Skip if enemy is dead
            if (data.isDeath)
                continue;

            if (data.navMeshAgent != null && data.targetPosition != null)
            {
                Vector3 targetPosition = data.targetPosition.position;
                float distanceToTarget = Vector3.Distance(targetPosition, data.navMeshAgent.transform.position);

                if (distanceToTarget > 1.8f)
                {
                    // Enemy should move towards the target
                    if (data.navMeshAgent.destination != targetPosition)
                    {
                        data.navMeshAgent.SetDestination(targetPosition);
                    }

                    // Ensure attack animation is off if enemy is moving
                    if (data.anim.GetBool("Attack"))
                    {
                        data.anim.SetBool("Attack", false);
                        // data.StopDamage();
                        // data.isApplyDamageToPlayer = false;
                    }

                    // Set attack state back to ready
                    data.isAttack = true;
                }
                else
                {
                    // Enemy is within attack range
                    if (data.isAttack && !data.anim.GetBool("Attack"))
                    {
                        data.anim.SetBool("Attack", true);

                        // Start a coroutine to repeatedly deal damage
                        // playerHandler.Zobmies(data);
                        // StartCoroutine(DealDamageRepeatedly(data.PlayerDamage()));
                        // Prevent multiple attack invocations
                        // data.isApplyDamageToPlayer = true;
                        //data.PlayerDamage();
                        data.isAttack = false;
                    }

                    /*  if (data.isApplyDamageToPlayer)
                      {
                          data.PlayerDamage();
                         // playerHandler.PlayerDamage(data);

                      }*/
                }
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogWarning($"Missing references for enemy: {pair.Key}");
#endif
            }
        }
    }
    // it will effect on when plyer shoots the zombie. Zombie health demage and it will die
    public void EnemyDeth(GameObject zombie)
    {
        if(PlayerHandler.isPlayerDead==true)
            return;
       // print(zombie.name);
        EnemyHealthDamege(zombie);
        if (enemyDictionary[zombie].health <= 0)
        {
            GameManager.kils++;
            print("Kill couont:: " + GameManager.kils);
            if (GameManager.kils == GameManager.target)
            {
                gameManager.LeveComplete();
            }
           
            zombie.GetComponent<CapsuleCollider>().enabled = false;
            zombie.transform.GetChild(0).GetChild(0).GetChild(2).
                GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).
                GetChild(0).GetComponent<CapsuleCollider>().enabled = false;
            enemyDictionary[zombie].anim.SetBool("Attack", false);
            if (scratchImg)
            {
                scratchImg.SetActive(false);
            }
            enemyDictionary[zombie].isDeath = true;
            enemyDictionary[zombie].navMeshAgent.isStopped = true;
            enemyDictionary[zombie].anim.SetTrigger("Death");
            _map.RemoveIcon(zombie.transform);
        }

        // StartCoroutine(BulletHit(newObj));
    }

    public void EnemyHealthDamege(GameObject zombie)
    {
        if (enemyDictionary[zombie].health <= 0)
            return;
        if (enemyDictionary[zombie].zombieType == EnemyData.ZombieType.Normal)
        {
            enemyDictionary[zombie].health -= 50;
        }
        if (enemyDictionary[zombie].zombieType == EnemyData.ZombieType.Medium)
        {
            enemyDictionary[zombie].health -= 40;
        }
        if (enemyDictionary[zombie].zombieType == EnemyData.ZombieType.Hard)
        {
            enemyDictionary[zombie].health -= 30;
        }
        // print(enemyDictionary[zombie].health);
    }


    private void OnApplicationPause(bool pause)
    {

        if (pause)
        {
            StopDamage();
        }
        /* else {
             Debug.Log("Pase to play");
             PauseToPlay();
         }*/

    }
    private void OnApplicationQuit()
    {
        StopDamage();
    }

    private void OnApplicationFocus(bool focus)
    {
        StopDamage();
    }
    private void OnIterateMethod()
    {
        StopDamage();
    }

    void StopDamage()
    {
        foreach (var pair in enemyDictionary)
        {
            EnemyData data = pair.Value;
            //data.StopDamage();
        }
    }
    void PauseToPlay()
    {
        foreach (var pair in enemyDictionary)
        {
            EnemyData data = pair.Value;
            if (data.anim.GetBool("Attack"))
            {
                // data.PlayerDamage();
            }
        }
    }


}
