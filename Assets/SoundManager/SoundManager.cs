using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private void Awake()
    {
        if(FindObjectsOfType<SoundManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    public GameObject audioPrefab;
    public List<AudioClip> audioClips;

    private void Update()
    {
        AudioSource audio = GetComponent<AudioSource>();
        if(Time.timeScale == 0)
        {
            audio.Pause();
        }
        else
        {
            audio.UnPause();
        }
    }


    public void PlaySound(int soundId)
    {
        try
        {
            AudioClip audioClip = audioClips[soundId];
            if(audioClip != null)
            {
                GameObject temp = Instantiate(audioPrefab);
                DontDestroyOnLoad(temp);
                temp.GetComponent<AudioSource>().clip = audioClip;
                temp.GetComponent<AudioSource>().Play();
                StartCoroutine(destroySource(temp));
            }
        }
        catch
        {
            Debug.Log("SoundManager: Something went wrong");
        }
    }
    IEnumerator destroySource(GameObject temp)
    {
        yield return new WaitForSecondsRealtime(temp.GetComponent<AudioSource>().clip.length);
        Destroy(temp);
    }
}
