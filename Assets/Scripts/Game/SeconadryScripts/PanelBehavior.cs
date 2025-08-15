using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PanelBehavior : MonoBehaviour
{

    public Image star1, star2, star3;

    [SerializeField] Sprite _filledStar;
    [SerializeField] Sprite _emptyStar;

    public void SetStars(int stars)
    {
        if(stars == 1)
        {
            star1.sprite = _filledStar;
            star2.sprite = _emptyStar;
            star3.sprite = _emptyStar;
        }
        if (stars == 2)
        {
            star1.sprite = _filledStar;
            star2.sprite = _emptyStar;
            star3.sprite = _filledStar;
        }
        if (stars == 3)
        {
            star1.sprite = _filledStar;
            star2.sprite = _filledStar;
            star3.sprite = _filledStar;
        }
    }
}
