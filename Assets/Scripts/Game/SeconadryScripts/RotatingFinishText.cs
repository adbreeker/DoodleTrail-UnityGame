using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingFinishText : MonoBehaviour
{
    public float rotationSpeed;
    private void FixedUpdate()
    {
        transform.localRotation *= Quaternion.Euler(0,rotationSpeed,0);
    }
}
