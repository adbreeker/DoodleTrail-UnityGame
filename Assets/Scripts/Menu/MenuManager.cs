using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static int LvlSelected = 0;

    public TextMeshProUGUI StarCountText;
    public GameObject LvLSelectPanel, ScrollView;
    public GameObject LvlButtonPrefab;
    Levels levels;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        FindFirstObjectByType<SoundManager>().gameObject.GetComponent<AudioSource>().time = 0;
        if(!PlayerPrefs.HasKey("LvL0Status"))
        {
            PlayerPrefs.SetInt("LvL0Status", 0);
        }
        levels = new Levels();
        StarCountText.text = levels.CountStars().ToString();
        CreateButtons();
    }

    void CreateButtons()
    {

        for (int levelId = 0; levelId < levels.LevelsCount(); levelId++)
        {
            GameObject temp = Instantiate(LvlButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity, ScrollView.transform);
            temp.GetComponent<LvLButton>().SetUpLvLButton(levelId);
        }
    }

    // Buttons --------------------------------------------------------------------------------

    public void OpenLevelSelectPanel()
    {
        FindFirstObjectByType<SoundManager>().PlaySound(0);
        LvLSelectPanel.SetActive(true);
    }

    public void CloseLevelSelectPanel()
    {
        FindFirstObjectByType<SoundManager>().PlaySound(0);
        LvLSelectPanel.SetActive(false);
    }

    public void EndlessModeButton()
    {
        FindFirstObjectByType<SoundManager>().PlaySound(0);
        SceneManager.LoadScene("Endless");
    }

    public void ExitButton()
    {
        FindFirstObjectByType<SoundManager>().PlaySound(0);
        Application.Quit();
    }

}
