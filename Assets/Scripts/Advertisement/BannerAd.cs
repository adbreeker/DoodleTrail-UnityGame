using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class BannerAd : MonoBehaviour //working very rarely, better not use
{

    [SerializeField] string _androidAdUnitId = "Banner_Android";
    [SerializeField] string _iOSAdUnitId = "Banner_iOS";
    string _adUnitId = null;

    [Header("Banner add options:")]
    public bool showBanner = false;
    [SerializeField] BannerPosition _bannerPosition = BannerPosition.TOP_CENTER;

    void Awake()
    {
        // Get the Ad Unit ID for the current platform:
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSAdUnitId
            : _androidAdUnitId;

        // Set the banner position:
        Advertisement.Banner.SetPosition(_bannerPosition);
    }

    public void LoadBanner() //loading banner ad
    {
        //IMPORTANT! only load content AFTER initialization (AdsInitializer.cs)
        if (!AdsParams.bannerAvailable && Advertisement.isInitialized)
        {
            // Set up options to notify the SDK of load events:
            BannerLoadOptions options = new BannerLoadOptions
            {
                loadCallback = OnBannerLoaded,
                errorCallback = OnBannerError
            };

            // Load the Ad Unit with banner content:
            if (AdsParams.showAdsDebugLogs) Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Banner.Load(_adUnitId, options);
        }
    }

    void OnBannerLoaded() //setting ad availability to true and showing banner when loaded
    {
        if (AdsParams.showAdsDebugLogs) Debug.Log("Banner loaded");
        AdsParams.bannerAvailable = true;
        ShowBannerAd();
    }

    void OnBannerError(string message) //loading ad again on error
    {
        if (AdsParams.showAdsDebugLogs) Debug.Log($"Banner Error: {message}");
        AdsParams.bannerAvailable = false;
        LoadBanner();
    }

    void ShowBannerAd() //showing banner on the set location
    {
        if(AdsParams.bannerAvailable)
        {
            if (showBanner)
            {
                // Set up options to notify the SDK of show events:
                BannerOptions options = new BannerOptions
                {
                    clickCallback = OnBannerClicked,
                    hideCallback = OnBannerHidden,
                    showCallback = OnBannerShown
                };

                // Show the loaded Banner Ad Unit:
                AdsParams.bannerAvailable = false;
                if (AdsParams.showAdsDebugLogs) Debug.Log("Showing Ad: " + _adUnitId);
                Advertisement.Banner.Show(_adUnitId, options);
            }
        }
    }

    public void HideBannerAd() //hiding banner ad;
    {
        if (AdsParams.showAdsDebugLogs) Debug.Log("Hiding Ad: " + _adUnitId);
        Advertisement.Banner.Hide();
    }

    void OnBannerClicked() 
    {

    }
    void OnBannerShown() 
    {

    }
    void OnBannerHidden() 
    {

    }

}