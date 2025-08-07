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
    public TextMeshProUGUI timer, scoreCounter, scorePause, scoreFail, bestScore;

    float distance = 0;
    bool uiPermision = false;

    int starsCollected = 0;

    bool newBest = false;

  

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
            CountScore();
        }    

        if (Input.GetKey(KeyCode.Escape) && uiPermision)
        {
            FindFirstObjectByType<SoundManager>().PlaySound(0);
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
        uiPermision = true;
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
                if(distance > 200)
                {
                    howMany = Random.Range(2, 4);
                }
                if(distance > 800)
                {
                    howMany = Random.Range(2, 5);
                }
                int[] positions = RandomExtensions.getUniqueRandomArray(0, obstacleSpawnPoints.Count, howMany);

                SpawnObstacle(positions, null);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    void SpawnObstacle(int[] positions, int? type)
    {
        for(int i = 0; i<positions.Length; i++)
        {
            int obsType;
            if (type==null)
            {
                obsType = Random.Range(0, obstaclePrefabs.Count);
            }
            else
            {
                obsType = (int)type;
            }

            Instantiate(obstaclePrefabs[obsType], obstacleSpawnPoints[positions[i]].position, Quaternion.identity);
            if(obsType == 0)
            {
                break;
            }
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

    void CountScore()
    {
        int score = (int)distance * starsCollected;

        scoreCounter.text = "Score:\n"
            + ((int)distance).ToString() + " m x " + starsCollected.ToString() +
            "<sprite=0>\n<color=yellow>" +  score;
        scorePause.text = "Current score:\n<color=yellow>" + (int)distance * starsCollected;
        scoreFail.text= "Failed with score:\n<color=yellow>" + (int)distance * starsCollected;

        if(!PlayerPrefs.HasKey("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", 0);
        }
        else
        {
            if(PlayerPrefs.GetInt("BestScore") < score)
            {
                newBest = true;
                PlayerPrefs.SetInt("BestScore", score);
            }

            bestScore.text = "Best Score:\n<color=yellow>" + PlayerPrefs.GetInt("BestScore");
        }
    }

    public void StarCollected()
    {
        starsCollected++;
    }

    IEnumerator LvLFailed(float deley)
    {
        FindFirstObjectByType<SoundManager>().PlaySound(2);
        yield return new WaitForSecondsRealtime(deley);
        Time.timeScale = 0;
        if(newBest)
        {
            scoreFail.text += "\nNew Best!";
        }
        failPanel.SetActive(true);
    }

    //buttons
    public void UnPauseButton()
    {
        FindFirstObjectByType<SoundManager>().PlaySound(0);
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void ReturnToMenuButton()
    {
        FindFirstObjectByType<SoundManager>().PlaySound(0);
        SceneManager.LoadScene("Menu");
    }

    public void ReloadButton()
    {
        FindFirstObjectByType<SoundManager>().PlaySound(0);
        SceneManager.LoadScene("Endless");
    }
}
