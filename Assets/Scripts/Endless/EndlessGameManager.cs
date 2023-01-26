using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndlessGameManager : MonoBehaviour
{
    public GameObject player;
    public Camera cam;
    public Drawer drawer;
    public List<GameObject> obstaclePrefabs = new List<GameObject>();
    public List<Transform> obstacleSpawnPoints = new List<Transform>();

    public GameObject pausePanel, failPanel;
    public TextMeshProUGUI timer;

    float distance = 0;

  

    void Start()
    {
        Time.timeScale = 1;
        drawer.drawPermision = false;
        StartCoroutine("CountStartGame");
    }

    void FixedUpdate()
    {
        if(player != null)
        {
            MoveCamera();
            CountDistance();
        }    

        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
    }

    IEnumerator CountStartGame()
    {
        yield return new WaitForSeconds(0.05f);
        player.GetComponent<PlayerEndless>().StartPlayer();
        for (int i=3; i>0; i--)
        {
            timer.text = i.ToString();
            timer.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
        }
        timer.text = "GO";
        yield return new WaitForSeconds(0.5f);
        timer.gameObject.SetActive(false);
        drawer.drawPermision = true;
        StartCoroutine("SpawnObstacles");
    }

    IEnumerator SpawnObstacles()
    {
        float nextObstacleOn = distance + 20;
        while(player != null)
        {
            if(player.transform.position.x >= nextObstacleOn)
            {
                nextObstacleOn = distance + Random.Range(20f, 40f);
                int howMany = Random.Range(1, 4);
                int[] positions = RandomExtensions.getUniqueRandomArray(0, obstacleSpawnPoints.Count, howMany);

                SpawnObstacle(positions, Random.Range(0, obstaclePrefabs.Count));
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    void SpawnObstacle(int[] positions, int type)
    {
        for(int i = 0; i<positions.Length; i++)
        {
            Instantiate(obstaclePrefabs[type], obstacleSpawnPoints[positions[i]].position, Quaternion.identity);
        }
    }

    void MoveCamera()
    {
        if (player != null)
        {
            if(cam.transform.position.x < player.transform.position.x + 6)
            {
                Vector3 pos = cam.transform.position;
                pos.x = player.transform.position.x + 6;
                cam.transform.position = pos;
            }
        }
    }

    void CountDistance()
    {
        if(player.transform.position.x > distance)
        {
            distance = player.transform.position.x;
        }
    }

    IEnumerator LvLFailed(float deley)
    {
        yield return new WaitForSecondsRealtime(deley);
        Time.timeScale = 0;
        failPanel.SetActive(true);
    }

    //buttons
    public void UnPauseButton()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void ReturnToMenuButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ReloadButton()
    {
        SceneManager.LoadScene("Endless");
    }
}
