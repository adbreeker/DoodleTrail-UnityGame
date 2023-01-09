using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingGround : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform playerSpawnPoint;

    public GameObject SpawnPlayer()
    {
        GameObject player = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        return player;
    }
}
