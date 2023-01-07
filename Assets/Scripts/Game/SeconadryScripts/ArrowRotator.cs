using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotator : MonoBehaviour
{
    Color actuallColor;
    Color red = new Color(0.91f, 0.24f, 0.19f, 1.0f);
    Color green = new Color(0.36f, 0.63f, 0.09f, 1.0f);
    public void Start()
    {
        actuallColor = gameObject.GetComponent<SpriteRenderer>().color;
    }
    public void Rotate()
    {
        gameObject.transform.Rotate(new Vector3(0, 0, 180));
        if(actuallColor == red)
        {
            actuallColor = green;
        }
        else
        {
            actuallColor = red;
        }
        actuallColor = gameObject.GetComponent<SpriteRenderer>().color = actuallColor;
    }
}
