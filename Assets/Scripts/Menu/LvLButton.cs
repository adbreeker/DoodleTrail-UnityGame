using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LvLButton : MonoBehaviour
{
    int lvl_id;
    int lvl_status;
    public TextMeshProUGUI button_lvl_text;
    public GameObject star1, star2, star3;

    public Sprite openButton, lockedButton;

    public void SetUpLvLButton(int id)
    {
        lvl_id = id;
        lvl_status = Levels.GetLvLStatus(lvl_id);

        if (lvl_status >= 0)
        {
            SetUnlockedButton();
        }
        else
        {
            SetLockedButton();
        }
    }

    
   

    void SetUnlockedButton()
    {
        gameObject.GetComponent<Button>().interactable = true;
        gameObject.GetComponent<Image>().sprite = openButton;
        button_lvl_text.text = lvl_id.ToString();

        star1.SetActive(true);
        star2.SetActive(true);
        star3.SetActive(true);

        if(lvl_status >= 1)
        {
            star1.GetComponent<Image>().color = new Color(1, 1, 0, 1);
        }
        if (lvl_status >= 2)
        {
            star2.GetComponent<Image>().color = new Color(1, 1, 0, 1);
        }
        if (lvl_status == 3)
        {
            star3.GetComponent<Image>().color = new Color(1, 1, 0, 1);
        }
    }

    void SetLockedButton()
    {
        gameObject.GetComponent<Button>().interactable = false;
        gameObject.GetComponent<Image>().sprite = lockedButton;
        button_lvl_text.text = "";

        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
    }

    public void LoadLvl()
    {
        FindObjectOfType<SoundManager>().PlaySound(0);
        MenuManager.LvlSelected = lvl_id;
        SceneManager.LoadScene("Game");
    }

    
}
