using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    // Public
    public static InterstitialAds Instance;

    // Serialize
    //[SerializeField] private string _androidAdID = "Interstitial_Android";
    //[SerializeField] private string _iOSAdID = "Interstitial_iOS";
    [SerializeField] private string _androidAdID = "Rewarded_Android";
    [SerializeField] private string _iOSAdID = "Rewarded_iOS";

    // Private
    private string _adID;

    private GameController _gameController;

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
        LoadAd();
    }

    private void OnEnable()
    {
        GameController.OnStart += FindGameController;
    }

    private void OnDisable()
    {
        GameController.OnStart -= FindGameController;
    }

    private void FindGameController()
    {
        _gameController = FindObjectOfType<GameController>();
    }

    public void LoadAd()
    {
        Debug.Log("Loading Ad: " + _adID);
        Advertisement.Load(_adID, this);
    }

    public void ShowAd()
    {
        Debug.Log("Showing Ad: " + _adID);
        Advertisement.Show(_adID, this);

        _gameController.PauseGame();
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        throw new System.NotImplementedException();
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
        LoadAd();

        _gameController.UnpauseGame();
    }
}
