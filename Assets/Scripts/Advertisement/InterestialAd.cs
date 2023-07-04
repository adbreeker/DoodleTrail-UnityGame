using UnityEngine;
using UnityEngine.Advertisements;

public class InterestialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOsAdUnitId = "Interstitial_iOS";
    string _adUnitId;

    public static bool addLoaded = false;

    void Awake()
    {
        // Get the Ad Unit ID for the current platform:
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsAdUnitId
            : _androidAdUnitId;
    }

    // Load content to the Ad Unit:
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        if(!addLoaded)
        {
            Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load(_adUnitId, this);
        }
    }

    // Show the loaded content in the Ad Unit:
    public void ShowAd()
    {
        // Note that if the ad content wasn't previously loaded, this method will fail
        if(addLoaded)
        {
            Debug.Log("Showing Ad: " + _adUnitId);
            Advertisement.Show(_adUnitId, this);
            addLoaded = false;
        }
    }

    // Implement Load Listener and Show Listener interface methods: 
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        addLoaded = true;
        // Optionally execute code if the Ad Unit successfully loads content.
    }

    public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
        LoadAd();
    }

    public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
        addLoaded = false;
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
