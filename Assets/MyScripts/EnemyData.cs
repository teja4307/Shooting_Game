using UnityEngine;
using UnityEngine.AI;
using System.Collections;
//using System.Threading;
public class EnemyData
{
    public string name;
    public float health;
    public float maxHealth = 100f; // Default max health
    public Transform targetPosition;
    public bool isDeath = false;
    public bool isAttack = true;
    public Animator anim;
    public NavMeshAgent navMeshAgent;
    //private Timer damageTimer;
    public enum ZombieType
    {
        Normal,
        Medium,
        Hard
    }

    public ZombieType zombieType;

    public EnemyData(string name, float health, Transform targetPosition, NavMeshAgent navMeshAgent, Animator enemyAnim, ZombieType zombieType)
    {
        this.name = name;
        this.health = health;
        this.maxHealth = health; // Set max health during initialization
        this.targetPosition = targetPosition;
        this.anim = enemyAnim;
        this.navMeshAgent = navMeshAgent;
        this.zombieType = zombieType;
    }

    public float GetHealthPercentage()
    {
        return health / maxHealth; // Calculate health percentage based on maxHealth
    }

    /* public void PlayerDamage()
     {
       ///  Debug.Log("one");
         if (!isAttack || isDeath)
             return;
        // Debug.Log("two");
         // Initialize the timer to call ApplyDamage every 2 seconds
         damageTimer = new Timer(_ => ApplyDamage(), null, 1000, 2000);
     }*/

    public void ApplyDamage()
    {
        // Debug.Log("three");
        /* if (!isDeath || !isAttack)
         {
             // Stop the timer if conditions are not met
             damageTimer?.Dispose();
             return;
         }*/
        if (isDeath)
        {
            //  StopDamage();
            return;
        }
        //  Debug.Log("four");
        int damage = zombieType switch
        {
            ZombieType.Normal => 10,
            ZombieType.Medium => 20,
            ZombieType.Hard => 30,
            _ => 0
        };
        PlayerHealthBar(damage);

        //PlayerHandler.health -= damage;
        // Debug.Log("Five");
        // Console.WriteLine($"{nameof(ZombieAttack)} dealt {damage} damage to the player. Player health: {PlayerHandler.health}");
        // Debug.Log($"{name} dealt {damage} damage to the player. Player health: {PlayerHandler.health}");

    }
    /*    private void ApplyDamage()
        {
            // Check conditions
            if (isDeath)
            {
                StopDamage();
                return;
            }

            Debug.Log($"ApplyDamage called. isDeath: {isDeath}, zombieType: {zombieType}");

            // Determine damage based on zombie type
            int damage = zombieType switch
            {
                ZombieType.Normal => 10,
                ZombieType.Medium => 20,
                ZombieType.Hard => 30,
                _ => 0
            };

            // Check health bar validity before applying damage
            if (PlayerHandler.health_img_bar == null)
            {
                Debug.LogError("Health bar Image is not assigned in PlayerHandler.");
                return;
            }

            // Apply damage and update health bar
            PlayerHealthBar(damage);

            Debug.Log("Damage applied successfully.");
        }*/


    /*  public void StopDamage()
      {
          // Call this to stop the timer
          damageTimer?.Dispose();
      }*/

    #region PlayerHealthBar
    public void PlayerHealthBar(int damage)
    {
        // Debug.Log("Check one");
        // Apply damage and clamp health
        PlayerHandler.health -= damage;
        PlayerHandler.health = Mathf.Clamp(PlayerHandler.health, 0, 100);
        //Debug.Log("Check one:: "+ PlayerHandler.health);

        // Validate health bar reference
        if (PlayerHandler.health_img_bar == null)
        {
            // Debug.LogError("Health bar Image is not assigned.");
            return;
        }

        //Debug.Log("Check two:: " + PlayerHandler.health);
        // Update health bar fill amount
        // Debug.Log("Check two:: " + healthD);

        //Debug.Log("Check thre:: " + PlayerHandler.health_img_bar.fillAmount);
        // Debug to verify updates
        // Debug.Log($"Health updated: {PlayerHandler.health}, Fill amount: {healthD}");
    }

    public IEnumerator PlayerHealthBarLerp()
    {
        float healthD = PlayerHandler.health / 100f;
        float elapsed = 0f;
        while (elapsed < 0.5f)
        {
            elapsed += Time.deltaTime;
            PlayerHandler.health_img_bar.fillAmount = Mathf.Lerp(PlayerHandler.health_img_bar.fillAmount, healthD, elapsed / 0.5f);
            yield return null;
        }
        PlayerHealthBarColor();
    }
    private void PlayerHealthBarColor()
    {
        if (PlayerHandler.health_img_bar.fillAmount > 0.70 && PlayerHandler.health_img_bar.fillAmount < 1)
        {
            if (ColorUtility.TryParseHtmlString(PlayerHandler.color[0], out Color newColor))
            {
                PlayerHandler.health_img_bar.color = newColor;
            }
        }
        if (PlayerHandler.health_img_bar.fillAmount > 0.40 && PlayerHandler.health_img_bar.fillAmount < 0.7)
        {
            if (ColorUtility.TryParseHtmlString(PlayerHandler.color[1], out Color newColor))
            {
                PlayerHandler.health_img_bar.color = newColor;
            }
        }
        if (PlayerHandler.health_img_bar.fillAmount > 0 && PlayerHandler.health_img_bar.fillAmount < 0.4)
        {
            if (ColorUtility.TryParseHtmlString(PlayerHandler.color[2], out Color newColor))
            {
                PlayerHandler.health_img_bar.color = newColor;
            }
        }
    }
    #endregion
}
