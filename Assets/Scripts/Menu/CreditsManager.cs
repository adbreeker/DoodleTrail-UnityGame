using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    [SerializeField]  RectTransform _buttonExit;
    bool _allowToExit = false;

    void Start()
    {
        Time.timeScale = 1;
        SoundManager.Instance.PlaySound(SoundEnum.FINISH_WIN, SoundType.GetType_OneShotSingleUse());
        _allowToExit = false;

        if (!PlayerPrefs.HasKey("Rated"))
        {
            PlayerPrefs.SetInt("Rated", 0);
        }
        
        if(PlayerPrefs.GetInt("Rated") == 0)
        {
            StartCoroutine(AllowExitAfterDelay(10f));
        }
        else
        {
            _allowToExit = true;
        }
    }

    IEnumerator AllowExitAfterDelay(float delay)
    {
        TextMeshProUGUI buttonText = _buttonExit.GetComponentInChildren<TextMeshProUGUI>();
        float timeElapsed = 0f;
        while(timeElapsed < delay)
        {
            buttonText.text = "No ratey no? (" + Mathf.CeilToInt(delay - timeElapsed) + "s)";
            yield return null;
            timeElapsed += Time.deltaTime;
        }
        buttonText.text = "Exit to Menu";
        _allowToExit = true;
    }

    public void Button_Menu()
    {
        SoundManager.Instance.PlaySound(SoundEnum.UI_BUTTON, SoundType.GetType_OneShotUI());

        if(_allowToExit)
        {
            SceneManager.LoadScene("Menu");
        }
        else
        {
            _buttonExit.localRotation *= Quaternion.Euler(0, 0, 180);
        }
    }

    public void Button_Rate()
    {
        SoundManager.Instance.PlaySound(SoundEnum.UI_BUTTON, SoundType.GetType_OneShotUI());
        PlayerPrefs.SetInt("Rated", 1);

        string packageName = Application.identifier;
        string marketUrl = "market://details?id=" + packageName;
        try
        {
            Application.OpenURL(marketUrl);
        }
        catch
        {
            Application.OpenURL("https://play.google.com/store/apps/details?id=" + packageName);
        }
    }
}
