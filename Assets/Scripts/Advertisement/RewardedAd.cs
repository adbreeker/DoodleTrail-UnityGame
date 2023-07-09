using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.Events;

public class RewardedAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    string _adUnitId = null;

    [Header("Reward methods:")]
    public UnityEvent rewardMethods;

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
        if (!AdsParams.rewardedAvailable && Advertisement.isInitialized)
        {
            if (AdsParams.showAdsDebugLogs) Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load(_adUnitId, this);
        }
    }

    public void OnUnityAdsAdLoaded(string adUnitId) //setting ad availability to true when ad loaded
    {
        if (AdsParams.showAdsDebugLogs) Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId.Equals(_adUnitId))
        {
            AdsParams.rewardedAvailable = true;
        }
    }

    public void ShowAd() //showing ad
    {
        if(AdsParams.rewardedAvailable)
        {
            if (AdsParams.showAdsDebugLogs) Debug.Log("Showing Ad: " + _adUnitId);
            Advertisement.Show(_adUnitId, this);
        }
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState) //rewarding player after ad show complete
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            if (AdsParams.showAdsDebugLogs) Debug.Log("Unity Ads Rewarded Ad Completed");
            //granting reward for watched ad
            rewardMethods.Invoke();
        }
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message) //loading ad again after ad load failure
    {
        if (AdsParams.showAdsDebugLogs) Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        LoadAd();
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message) //loading ad again after ad show failure
    {
        if (AdsParams.showAdsDebugLogs) Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        LoadAd();
    }

    public void OnUnityAdsShowStart(string adUnitId) 
    { 

    }
    public void OnUnityAdsShowClick(string adUnitId)
    {

    }

}

