using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerTutorial : MonoBehaviour
{
    void Update()
    {
        Vector3 pos = transform.localPosition;
        if(pos.x  > 175)
        {
            pos.x = -175;
        }
        else
        {
            pos.x+=200 * Time.unscaledDeltaTime;
        }
        transform.localPosition = pos;
    }
}
