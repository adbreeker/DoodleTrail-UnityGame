using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static int LvlSelected = 0;

    public TextMeshProUGUI StarCountText;
    public GameObject LvLSelectPanel;
    public RectTransform ScrollView;
    public RectTransform LevelButtonsHolder;
    public GameObject LvlButtonPrefab;
    Levels levels;


    void Start()
    {
        Time.timeScale = 1;
        SoundManager.Instance.GetComponent<AudioSource>().time = 0;
        if(!PlayerPrefs.HasKey("LvL0Status"))
        {
            PlayerPrefs.SetInt("LvL0Status", 0);
        }
        levels = new Levels();
        StarCountText.text = levels.CountStars().ToString();
        CreateButtons();
    }

    void Update()
    {
        if(LvLSelectPanel.activeSelf)
        {
            SetupScrollView();
        }
    }

    void SetupScrollView()
    {
        float scrollViewHeight = 0;
        foreach (RectTransform child in ScrollView)
        {
            scrollViewHeight += child.rect.height;
            LayoutGroup layout = child.GetComponent<LayoutGroup>();
            if (layout != null)
            {
                if (layout is HorizontalLayoutGroup h)
                {
                    scrollViewHeight += h.spacing;
                }
                else if (layout is VerticalLayoutGroup v)
                {
                    scrollViewHeight += v.spacing;
                }
                else if (layout is GridLayoutGroup g)
                {
                    scrollViewHeight += g.spacing.y;
                }
            }
        }

        Vector2 newSize = ScrollView.sizeDelta;
        newSize.y = scrollViewHeight;
        ScrollView.sizeDelta = newSize;
    }

    void CreateButtons()
    {

        for (int levelId = 0; levelId < levels.LevelsCount(); levelId++)
        {
            GameObject temp = Instantiate(LvlButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity, LevelButtonsHolder);
            temp.GetComponent<LvLButton>().SetUpLvLButton(levelId);
        }
    }

    // Buttons --------------------------------------------------------------------------------

    public void OpenLevelSelectPanel()
    {
        SoundManager.Instance.PlaySound(SoundEnum.UI_BUTTON, SoundType.GetType_OneShotUI());
        LvLSelectPanel.SetActive(true);
    }

    public void CloseLevelSelectPanel()
    {
        SoundManager.Instance.PlaySound(SoundEnum.UI_BUTTON, SoundType.GetType_OneShotUI());
        LvLSelectPanel.SetActive(false);
    }

    public void CreditsButton()
    {
        SoundManager.Instance.PlaySound(SoundEnum.UI_BUTTON, SoundType.GetType_OneShotUI());
        SceneManager.LoadScene("Credits");
    }

    public void EndlessModeButton()
    {
        SoundManager.Instance.PlaySound(SoundEnum.UI_BUTTON, SoundType.GetType_OneShotUI());
        SceneManager.LoadScene("Endless");
    }

    public void ExitButton()
    {
        SoundManager.Instance.PlaySound(SoundEnum.UI_BUTTON, SoundType.GetType_OneShotUI());
        Application.Quit();
    }

}
