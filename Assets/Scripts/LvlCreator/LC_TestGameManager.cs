using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LC_TestGameManager : MonoBehaviour
{
    //game
    public Drawer drawer;
    
    GameObject startingGround, player;

    //ui
    [Space(30.0f)]
    public GameObject pausePanel;
    public Button startB, reloadB, undoB, reverseB, startModeB;
    
    

    [Header("Level Creator")]
    public LevelCreator levelCreator;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        startingGround = levelCreator.startingGround;
        player = startingGround.GetComponent<StartingGround>().SpawnPlayer();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (drawer.linesCount > 0)
        {
            undoB.interactable = true;
        }
        else
        {
            undoB.interactable = false;
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }

    }


    //buttons ------------------------------------------------------------------------------------------
    public void StartButton()
    {
        startB.gameObject.SetActive(false);
        reloadB.gameObject.SetActive(true);
        reverseB.interactable = false;
        startModeB.interactable = false;
        player.GetComponent<Player>().StartPlayer();
    }

    public void ReloadButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UndoButton()
    {
        drawer.UndoLine();
    }

    public void ReverseButton()
    {
        player.GetComponent<Player>().ReverseForce();
        foreach (ArrowRotator rotator in startingGround.GetComponentsInChildren<ArrowRotator>())
        {
            rotator.Rotate();
        }
    }

    public void StartModeButton()
    {
        player.GetComponent<Player>().StartMode *= -1;
    }


    public void UnPauseButton()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }


    // game -----------------------------------------------------------------------------------------------

    public void LvLCompleted()
    {
        Time.timeScale = 0;

        startB.interactable = false;
        reloadB.interactable = false;
        undoB.interactable = false;
        reverseB.interactable = false;
        startModeB.interactable = false;
    }
}
