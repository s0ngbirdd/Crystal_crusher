using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAds : MonoBehaviour
{
    // Serialize
    [SerializeField] private BannerPosition _bannerPosition;
    [SerializeField] private string _androidAdID = "Banner_Android";
    [SerializeField] private string _iOSAdID = "Banner_iOS";

    // Private
    private string _adID;

    private void Awake()
    {
        _adID = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOSAdID : _androidAdID;
    }

    private void Start()
    {
        Advertisement.Banner.SetPosition(_bannerPosition);
        StartCoroutine(LoadAdBanner());
    }

    private IEnumerator LoadAdBanner()
    {
        yield return new WaitForSeconds(1f);
        LoadBanner();
    }

    public void LoadBanner()
    {
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        Advertisement.Banner.Load(_adID, options);
    }

    private void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");
        ShowBannerAd();
    }

    private void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
        // Optionally execute additional code, such as attempting to load another ad.
    }

    public void ShowBannerAd()
    {
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        Advertisement.Banner.Show(_adID, options);
    }

    private void OnBannerClicked() { }

    private void OnBannerHidden() { }

    private void OnBannerShown() { }
}
