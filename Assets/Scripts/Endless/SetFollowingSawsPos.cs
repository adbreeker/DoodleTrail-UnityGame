using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFollowingSawsPos : MonoBehaviour
{
    void Start()
    {
        Camera cam = GetComponentInParent<Camera>();
        float x = cam.orthographicSize * cam.aspect;
        transform.position = new Vector3(-x, 0, 1);
    }
}
