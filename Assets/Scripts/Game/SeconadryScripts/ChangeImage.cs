using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    public Sprite engine, cannon;
    Sprite actuall;
    void Start()
    {
        actuall = gameObject.GetComponent<Image>().sprite;
    }

    public void SwitchImage()
    {
        if(actuall == engine)
        {
            gameObject.GetComponent<Image>().sprite = cannon;
            actuall = cannon;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = engine;
            actuall = engine;
        }
    }
}
