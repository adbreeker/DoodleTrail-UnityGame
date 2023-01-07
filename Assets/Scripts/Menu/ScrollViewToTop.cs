using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollViewToTop : MonoBehaviour
{
    private void OnEnable()
    {
        Vector3 pos = new Vector3(gameObject.transform.position.x, -1000 + ((gameObject.transform.childCount/5) * -325) , 0);
        gameObject.transform.position = pos;

    }
}
