using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //game
    public GameObject startPrefab;
    public GameObject finishPrefab;
    public Drawer drawer;
    Levels levels;
    GameObject startingGround, player;
    int lvl_id;
    [HideInInspector] public bool starCollected = false;
    [HideInInspector] public bool noMoreLines = true;
    [HideInInspector] public bool gameStarted = false;
    public ObstaclesListSO obstaclesListSO;

    //ui
    public Button startB, reloadB, undoB, reverseB, startModeB;
    public GameObject lvlCompletedPanel, pausePanel, failPanel;
    public Button nextLvLB;
    public GameObject playerPointer;

    [Header("Advertisement")]
    public GameObject adsManager;

    

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        levels = new Levels();
        lvl_id = MenuManager.LvlSelected;
        SpawnObjects();
        if(lvl_id == 0)
        {
            GetComponent<TutorialManager>().StartTutorial();
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(drawer.linesCount > 0)
        {
            undoB.interactable = true;
        }
        else
        {
            undoB.interactable = false;
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            SoundManager.Instance.PlaySound(SoundEnum.UI_BUTTON, SoundType.GetType_OneShotUI());
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }

    }


    //buttons ------------------------------------------------------------------------------------------
    public void StartButton()
    {
        SoundManager.Instance.PlaySound(SoundEnum.UI_BUTTON, SoundType.GetType_OneShotUI());
        gameStarted = true;
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

    public void BackToMenuButton()
    {
        SoundManager.Instance.PlaySound(SoundEnum.UI_BUTTON, SoundType.GetType_OneShotUI());
        SceneManager.LoadScene("Menu");
    }

    public void UnPauseButton()
    {
        SoundManager.Instance.PlaySound(SoundEnum.UI_BUTTON, SoundType.GetType_OneShotUI());
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void LoadNextLvLButton()
    {
        SoundManager.Instance.PlaySound(SoundEnum.UI_BUTTON, SoundType.GetType_OneShotUI());
        if (Random.Range(1,3) == 1)
        {
            adsManager.GetComponent<InterestialAd>().ShowAd();
        }
        MenuManager.LvlSelected = lvl_id + 1;
        SceneManager.LoadScene("Game");
    }

    // game -----------------------------------------------------------------------------------------------
    void SpawnObjects()
    {
        Levels.Level level = levels.getLevel(lvl_id);

        //player:
        Vector3 playerLoc = level.player.position;
        Quaternion playerRot = level.player.rotation;
        startingGround = Instantiate(startPrefab, playerLoc, playerRot);
        player = startingGround.GetComponent<StartingGround>().SpawnPlayer();

        player.GetComponent<Player>().playerPointer = playerPointer;
        playerPointer.GetComponent<PlayerPointer>().player = player;

        //finish:
        Vector3 finishLoc = level.finish.position;
        Quaternion finishRot = level.finish.rotation;
        Instantiate(finishPrefab, finishLoc, finishRot);

        //obstacles:
        foreach (Levels.Obstacle obstacle in levels.getLevel(lvl_id).obstacles)
        {
            Instantiate(obstaclesListSO.obstacles[obstacle.type], obstacle.position, obstacle.rotation);
        }
    }

    public void LvLCompleted()
    {
        SoundManager.Instance.PlaySound(SoundEnum.FINISH_WIN, SoundType.GetType_OneShotSingleUse());
        Time.timeScale = 0;
        if (lvl_id < levels.LevelsCount())
        {
            if (Levels.GetLvLStatus(lvl_id + 1) == -1)
            {
                PlayerPrefs.SetInt("LvL" + (lvl_id + 1).ToString() + "Status", 0);
            }
        }
        startB.interactable = false;
        reloadB.interactable = false;
        undoB.interactable = false;
        reverseB.interactable = false;
        startModeB.interactable = false;
        lvlCompletedPanel.SetActive(true);

        if (lvl_id == (levels.LevelsCount() - 1))
        {
            nextLvLB.interactable = false;
        }

        //manage lvl starts rate

        lvlCompletedPanel.GetComponent<PanelBehavior>().SetStars(1);
        if (Levels.GetLvLStatus(lvl_id) < 1)
        {
            PlayerPrefs.SetInt("LvL" + (lvl_id).ToString() + "Status", 1);
        }
        if (noMoreLines || starCollected)
        {
            lvlCompletedPanel.GetComponent<PanelBehavior>().SetStars(2);
            if (Levels.GetLvLStatus(lvl_id) < 2)
            {
                PlayerPrefs.SetInt("LvL" + (lvl_id).ToString() + "Status", 2);
            }
        }
        if (noMoreLines && starCollected)
        {
            lvlCompletedPanel.GetComponent<PanelBehavior>().SetStars(3);
            if (Levels.GetLvLStatus(lvl_id) < 3)
            {
                PlayerPrefs.SetInt("LvL" + (lvl_id).ToString() + "Status", 3);
            }
        }
    }

    IEnumerator LvLFailed(float deley)
    {
        SoundManager.Instance.PlaySound(SoundEnum.FINISH_FAIL, SoundType.GetType_OneShotSingleUse());
        yield return new WaitForSecondsRealtime(deley);
        Time.timeScale = 0;
        failPanel.SetActive(true);
    }
}
