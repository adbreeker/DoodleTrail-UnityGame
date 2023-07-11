using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaryEngine : MonoBehaviour
{
    public GameObject toRotate;
    public float rotationSpeed;
    void FixedUpdate()
    {
        toRotate.transform.rotation *= Quaternion.Euler(0, 0, rotationSpeed);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
