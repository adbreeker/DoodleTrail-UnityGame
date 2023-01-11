using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerTutorial : MonoBehaviour
{
    void Update()
    {
        Vector3 pos = transform.localPosition;
        if(pos.x  > 150)
        {
            pos.x = -150;
        }
        else
        {
            pos.x+=2;
        }
        transform.localPosition = pos;
    }
}
