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

    public Sprite openButton, lockedButton;

    public void SetUpLvLButton(int id)
    {
        lvl_id = id;
        lvl_status = GetLvLStatus("LvL" + lvl_id.ToString() + "Status");

        if (LvLUnlocked())
        {
            gameObject.GetComponent<Button>().interactable = true;
            gameObject.GetComponent<Image>().sprite = openButton;
            button_lvl_text.text = lvl_id.ToString();
        }
        else
        {
            gameObject.GetComponent<Button>().interactable = false;
            gameObject.GetComponent<Image>().sprite = lockedButton;
            button_lvl_text.text = "";
        }
    }

    int GetLvLStatus(string key)
    {
        int status;
        if(!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetInt(key, 0);
            status = 0;
        }
        else
        {
            status = PlayerPrefs.GetInt(key);
        }

        return status;
    }
    
    bool LvLUnlocked()
    {

        if(lvl_status > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void SetLocation(float x, float y)
    {
        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(x, y, 0);
        gameObject.SetActive(true);
    }

    public void LoadLvl()
    {
        MenuManager.LvlSelected = lvl_id;
        SceneManager.LoadScene("Game");
    }


    
}
