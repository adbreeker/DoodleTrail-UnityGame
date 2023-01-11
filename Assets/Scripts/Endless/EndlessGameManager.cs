using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndlessGameManager : MonoBehaviour
{
    public GameObject player;
    public Camera cam;
    public List<GameObject> obsaclePrefabs = new List<GameObject>();

    public GameObject pausePanel, failPanel;

    bool gameStarted = false;
    

    void Start()
    {
        Time.timeScale = 1;
        StartCoroutine("CountStartGame");
    }

    void FixedUpdate()
    {
        if(gameStarted)
        {
            Vector3 pos = cam.transform.position;
            pos.x = player.transform.position.x + 6;
            cam.transform.position = pos;
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
    }

    IEnumerator CountStartGame()
    {
        for(int i=4; i>=0; i--)
        {
            yield return new WaitForSeconds(1f);
        }
        player.GetComponent<PlayerEndless>().StartPlayer();
        gameStarted = true;
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
