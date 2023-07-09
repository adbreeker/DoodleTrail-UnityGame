using UnityEngine;
using UnityEngine.Advertisements;

public class InterestialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOSAdUnitId = "Interstitial_iOS";
    string _adUnitId = null;

    void Awake()
    {
        //get the Ad Unit ID for the current platform:
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSAdUnitId
            : _androidAdUnitId;
    }

    public void LoadAd() //loading ad 
    {
        //IMPORTANT! only load content AFTER initialization (AdsInitializer.cs)
        if (!AdsParams.interestialAvailable && Advertisement.isInitialized)
        {
            if (AdsParams.showAdsDebugLogs) Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load(_adUnitId, this);
        }
    }

    public void ShowAd() //showing ad
    {
        if(AdsParams.interestialAvailable)
        {
            if (AdsParams.showAdsDebugLogs) Debug.Log("Showing Ad: " + _adUnitId);
            AdsParams.interestialAvailable = false;
            Advertisement.Show(_adUnitId, this);
        }
    }

    public void OnUnityAdsAdLoaded(string adUnitId) //setting ad availability to true when ad loaded
    {
        if (adUnitId.Equals(_adUnitId))
        {
            if (AdsParams.showAdsDebugLogs) Debug.Log("Ad Loaded: " + adUnitId);
            AdsParams.interestialAvailable = true;
        }
    }

    public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message) //loading ad again after ad load failure
    {
        if (AdsParams.showAdsDebugLogs) Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
        LoadAd();
    }

    public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message) //setting ad availability to false and loading ad again after ad show failure
    {
        if (AdsParams.showAdsDebugLogs) Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
        AdsParams.interestialAvailable = false;
        LoadAd();
    }

    public void OnUnityAdsShowStart(string _adUnitId) 
    {

    }
    public void OnUnityAdsShowClick(string _adUnitId) 
    { 

    }
    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState) 
    { 

    }
}
