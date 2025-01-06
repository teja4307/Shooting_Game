using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Edges edges;
    [SerializeField] private Transform worldT_Player_transform;
    [SerializeField] private RectTransform imageTransform_Plaeyr;


    [SerializeField] private RectTransform enmay_Ui_Icon;

    public List<Transform> world_Enemy_transform = new List<Transform>();
    [SerializeField] private List<RectTransform> imageTransform_Enemy = new List<RectTransform>();

    //[SerializeField] EnemyHandler enemyHandler;

    private RectTransform MapTransform => transform as RectTransform;


    private void Awake()
    {
       //  MapTransform = transform as RectTransform;
        //  EnemyMapIconsInst(4);

    }

    public void EnemyMapIconsInst(int count)
    {

        for (int i = 0; i < count; i++)
        {
            RectTransform e_Icon = Instantiate(enmay_Ui_Icon, transform);
            imageTransform_Enemy.Add(e_Icon);
        }
    }

    void EnemyAnchorPositonUpdate(List<Transform> world_Enemy_transform)
    {
        for (int i = 0; i < world_Enemy_transform.Count; i++)
        {
            if (world_Enemy_transform[i] != null)
            {
                imageTransform_Enemy[i].anchoredPosition = FindInterfacePoint(world_Enemy_transform[i]);
            }
        }
    }

    public void RemoveIcon(Transform _worldObj)
    {
        int index = world_Enemy_transform.IndexOf(_worldObj);

        // world_Enemy_transform.RemoveAt(index);
        // imageTransform_Enemy.RemoveAt(index);
        imageTransform_Enemy[index].gameObject.SetActive(false);
    }
    private void Update()
    {
        // Update the anchored position of the imageTransform.
        if (worldT_Player_transform != null)
            imageTransform_Plaeyr.anchoredPosition = FindInterfacePoint(worldT_Player_transform);

        EnemyAnchorPositonUpdate(world_Enemy_transform);
        //if(worldT_Enemy_transform !=null)
        // imageTransform_Enemy.anchoredPosition= FindInterfacePoint(worldT_Enemy_transform);


    }

    /// <summary>
    /// Finds the point on the UI map corresponding to a world position.
    /// </summary>
    /// <returns>A Vector2 position in the local space of the map's RectTransform.</returns>
    private Vector2 FindInterfacePoint(Transform _WorldTrnsform)
    {
        // Get the normalized position in the world bounds.
        Vector2 normalizedPosition = edges.FindNormalizedPosition(_WorldTrnsform.position);

        // Convert the normalized position to a local position on the map.
        Vector2 localPoint = Rect.NormalizedToPoint(MapTransform.rect, normalizedPosition);

        // Adjust the local point to consider the MapBG structure.
        localPoint.x = Mathf.Lerp(0, MapTransform.rect.width, normalizedPosition.x) - MapTransform.rect.width / 2;
        localPoint.y = Mathf.Lerp(0, MapTransform.rect.height, normalizedPosition.y) - MapTransform.rect.height / 2;

        // Return the adjusted local point.
        return localPoint;
    }
}