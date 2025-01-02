using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSwipe : MonoBehaviour
{
    public GameObject scroll_Bar;
    float scroll_Pos = 0;
   public  float[] pos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
        if (Input.GetMouseButton(0))
        {
            scroll_Pos = scroll_Bar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_Pos < pos[i] + (distance / 2) && scroll_Pos > pos[i] - (distance / 2))
                {
                    scroll_Bar.GetComponent<Scrollbar>().value = Mathf.Lerp(scroll_Bar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }
        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_Pos < pos[i] + (distance / 2) && scroll_Pos > pos[i] - (distance / 2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);
                for(int a = 0; a < pos.Length; a++)
                {
                    if (a != i)
                    {
                        transform.GetChild(a).localScale = Vector2.Lerp(transform.GetChild(a).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                    }
                }
            }
        }
    }
}
