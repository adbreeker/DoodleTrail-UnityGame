using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject tut_Info, tut_Reverse, tut_Mode, tut_Drawing, tut_Undo, tut_Start;

    GameObject[] tut_hierarchy;
    int actuallTutorial = 0;

    public void StartTutorial()
    {
        Time.timeScale = 0;
        GameObject[] tutorials = { tut_Info, tut_Reverse, tut_Mode, tut_Drawing, tut_Undo, tut_Start };
        tut_hierarchy = tutorials;
        actuallTutorial = 0;
        DisableButtons();
        tut_hierarchy[actuallTutorial].SetActive(true);
    }

    void StopTutorial()
    {
        EnableButtons();
        Time.timeScale = 1;
    }

    void DisableButtons()
    {
        GameManager gm = GetComponent<GameManager>();
        gm.startB.interactable = false;
        gm.reloadB.interactable = false;
        gm.undoB.interactable = false;
        gm.startModeB.interactable = false;
        gm.reverseB.interactable = false;
    }

    void EnableButtons()
    {
        GameManager gm = GetComponent<GameManager>();
        gm.startB.interactable = true;
        gm.reloadB.interactable = true;
        gm.undoB.interactable = true;
        gm.startModeB.interactable = true;
        gm.reverseB.interactable = true;
    }

    public void CloseTutorialButton()
    {
        SoundManager.Instance.PlaySound(SoundEnum.UI_BUTTON, SoundType.GetType_OneShotUI());
        tut_hierarchy[actuallTutorial].SetActive(false);
        if(actuallTutorial == tut_hierarchy.Length - 1)
        {
            StopTutorial();
        }
        else
        {
            actuallTutorial++;
            tut_hierarchy[actuallTutorial].SetActive(true);
        }
    }
}
