using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;

    InterestialAd interestialAd;
    RewardedAd rewardedAd;
    BannerAd bannerAd;

    void Awake()
    {
        interestialAd = GetComponent<InterestialAd>();
        rewardedAd = GetComponent<RewardedAd>();
        bannerAd = GetComponent<BannerAd>();
        InitializeAds();
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
        else if(Advertisement.isInitialized)
        {
            interestialAd.LoadAd();
            bannerAd.LoadBanner();
        }
    }


    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        interestialAd.LoadAd();
        bannerAd.LoadBanner();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        Advertisement.Initialize(_gameId, _testMode, this);
    }
}
