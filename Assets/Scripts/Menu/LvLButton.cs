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
    [SerializeField] GameObject _lockIcon;
    [SerializeField] TextMeshProUGUI _levelText;
    public GameObject star1, star2, star3;
    [SerializeField] Sprite _filledStar;

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
        _lockIcon.SetActive(false);
        _levelText.text = lvl_id.ToString();

        star1.SetActive(true);
        star2.SetActive(true);
        star3.SetActive(true);

        if(lvl_status >= 1)
        {
            star1.GetComponent<Image>().sprite = _filledStar;
        }
        if (lvl_status >= 2)
        {
            star2.GetComponent<Image>().sprite = _filledStar;
        }
        if (lvl_status == 3)
        {
            star3.GetComponent<Image>().sprite = _filledStar;
        }
    }

    void SetLockedButton()
    {
        gameObject.GetComponent<Button>().interactable = false;
        _lockIcon.SetActive(true);
        _levelText.text = "";

        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
    }

    public void LoadLvl()
    {
        SoundManager.Instance.PlaySound(SoundEnum.UI_BUTTON);
        MenuManager.LvlSelected = lvl_id;
        SceneManager.LoadScene("Game");
    }

    
}
