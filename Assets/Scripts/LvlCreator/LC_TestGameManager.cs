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
        SoundManager.Instance.PlaySound(SoundEnum.UI_BUTTON, SoundType.GetType_OneShotUI());
        startB.gameObject.SetActive(false);
        reloadB.gameObject.SetActive(true);
        reverseB.interactable = false;
        startModeB.interactable = false;
        player.GetComponent<Player>().StartPlayer();
    }

    public void ReloadButton()
    {
        SoundManager.Instance.PlaySound(SoundEnum.UI_BUTTON, SoundType.GetType_OneShotUI());
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UndoButton()
    {
        SoundManager.Instance.PlaySound(SoundEnum.UI_BUTTON, SoundType.GetType_OneShotUI());
        drawer.UndoLine();
    }

    public void ReverseButton()
    {
        SoundManager.Instance.PlaySound(SoundEnum.UI_BUTTON, SoundType.GetType_OneShotUI());
        player.GetComponent<Player>().ReverseForce();
        foreach (ArrowRotator rotator in startingGround.GetComponentsInChildren<ArrowRotator>())
        {
            rotator.Rotate();
        }
    }

    public void StartModeButton()
    {
        SoundManager.Instance.PlaySound(SoundEnum.UI_BUTTON, SoundType.GetType_OneShotUI());
        player.GetComponent<Player>().StartMode *= -1;
    }


    public void UnPauseButton()
    {
        SoundManager.Instance.PlaySound(SoundEnum.UI_BUTTON, SoundType.GetType_OneShotUI());
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
}
