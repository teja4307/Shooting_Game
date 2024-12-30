using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ZombieHealthGUI : MonoBehaviour
{
    public EnemyHandler _enemyHandler; // Reference to the zombie health
    public Vector2 offset = new Vector2(0, 2); // Offset above the zombie
    public Vector2 size = new Vector2(100, 10); // Size of the health bar
    public Color healthBarColor = Color.green; // Color of the health bar
    public Color backgroundColor = Color.red; // Background color of the bar

    private Camera mainCamera;

    private Dictionary<GameObject, EnemyData> enemyDictionary=new Dictionary<GameObject, EnemyData>();
    private void Start()
    {
        mainCamera = Camera.main;
        enemyDictionary = _enemyHandler.enemyDictionary;
    }

    private void OnGUI()
    {
        if (PlayerHandler.isPlayerDead == true) return;
        if (_enemyHandler == null) return;

        foreach (var pair in enemyDictionary)
        {
            GameObject zomby = pair.Key;
            EnemyData data = pair.Value;
            if (!data.isDeath)
            {
                // Get the zombie's screen position
                Vector3 screenPosition = mainCamera.WorldToScreenPoint(zomby.transform.position + (Vector3)offset);

                // Check if the zombie is in front of the camera
                if (screenPosition.z > 0)
                {
                    // Convert to GUI position (y is inverted in GUI space)
                    Vector2 guiPosition = new Vector2(screenPosition.x - size.x / 2, Screen.height - screenPosition.y);

                    // Draw the background bar
                    GUI.color = backgroundColor;
                    GUI.DrawTexture(new Rect(guiPosition, size), Texture2D.whiteTexture);

                    // Draw the health bar
                    float healthPercentage = data.GetHealthPercentage(); //50;//zombieHealth.GetHealthPercentage();
                    GUI.color = healthBarColor;
                    GUI.DrawTexture(new Rect(guiPosition, new Vector2(size.x * healthPercentage, size.y)), Texture2D.whiteTexture);
                }
            }
        }
    }
}
