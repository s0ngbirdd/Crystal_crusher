using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class RewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    // Public
    public static RewardedAds Instance;

    // Serialize
    [SerializeField] private Button _buttonShowAd;
    [SerializeField] private string _androidAdID = "Rewarded_Android";
    [SerializeField] private string _iOSAdID = "Rewarded_iOS";

    // Private
    private string _adID;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _adID = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOSAdID : _androidAdID;

        _buttonShowAd.interactable = false;
    }

    private void Start()
    {
        LoadAd();
    }

    public void LoadAd()
    {
        Debug.Log("Loading Ad: " + _adID);
        Advertisement.Load(_adID, this);
    }

    public void ShowAd()
    {
        _buttonShowAd.interactable = false;

        Advertisement.Show(_adID, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Ad Loaded: " + placementId);

        if (placementId.Equals(_adID))
        {
            _buttonShowAd.onClick.AddListener(ShowAd);

            _buttonShowAd.interactable = true;
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId.Equals(_adID) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
        }
    }

    private void OnDestroy()
    {
        _buttonShowAd.onClick.RemoveAllListeners();
    }
}
