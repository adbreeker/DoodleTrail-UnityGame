using UnityEngine;
using UnityEngine.Advertisements;

public static class AdsParams
{
    public static bool interestialAvailable = false;
    public static bool rewardedAvailable = false;
    public static bool bannerAvailable = false;

    //ads related debug logs
    public static bool showAdsDebugLogs = false;
}

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;

    [Header("Load ad types:")]
    public bool loadInterestial = false;
    public bool loadRewarded = false;
    public bool loadBanner = false;

    [Header("Ads related debug logs")]
    public bool adsDebugLogs = false;

    //ads controllers
    InterestialAd interestialAd;
    RewardedAd rewardedAd;
    BannerAd bannerAd;

    //prevent multiple ad loading
    bool adsAllreadyLoading = false;

    void Awake()
    {
        AdsParams.showAdsDebugLogs = adsDebugLogs;
        interestialAd = GetComponent<InterestialAd>();
        rewardedAd = GetComponent<RewardedAd>();
        bannerAd = GetComponent<BannerAd>();
        InitializeAds();
    }

    void Start()
    {
        if(Advertisement.isInitialized && !adsAllreadyLoading)
        {
            if(AdsParams.showAdsDebugLogs) Debug.Log("Loading ads from start");
            adsAllreadyLoading=true;
            LoadRequestedAdds();
        }
    }

    public void InitializeAds()
    {
#if UNITY_IOS
            _gameId = _iOSGameId;
#elif UNITY_ANDROID
        _gameId = _androidGameId;
#elif UNITY_EDITOR
            _gameId = _androidGameId; //Only for testing the functionality in the Editor
            _testMode = true;
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }


    public void OnInitializationComplete()
    {
        if (AdsParams.showAdsDebugLogs) Debug.Log("Unity Ads initialization complete.");
        if(!adsAllreadyLoading)
        {
            if (AdsParams.showAdsDebugLogs) Debug.Log("Loading ads from init completed");
            adsAllreadyLoading =true;
            LoadRequestedAdds();
        }
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        if (AdsParams.showAdsDebugLogs) Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void LoadRequestedAdds()
    {
        if (loadInterestial)
        {
            interestialAd.LoadAd();
        }
        if (loadRewarded)
        {
            rewardedAd.LoadAd();
        }
        if (loadBanner)
        {
            bannerAd.LoadBanner();
        }
    }
}
