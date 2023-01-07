using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PanelBehavior : MonoBehaviour
{

    public Image star1, star2, star3;

    public void SetStars(int stars)
    {
        if(stars == 1)
        {
            star1.color = new Color(1, 1, 0, 1);
            star2.color = new Color(1, 1, 1, 1);
            star3.color = new Color(1, 1, 1, 1);
        }
        if (stars == 2)
        {
            star1.color = new Color(1, 1, 0, 1);
            star2.color = new Color(1, 1, 0, 1);
            star3.color = new Color(1, 1, 1, 1);
        }
        if (stars == 3)
        {
            star1.color = new Color(1, 1, 0, 1);
            star2.color = new Color(1, 1, 0, 1);
            star3.color = new Color(1, 1, 0, 1);
        }
    }
}
