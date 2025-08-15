using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Obstacle name")]
    public string obstacleName;

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
