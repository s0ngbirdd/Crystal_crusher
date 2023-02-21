using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    // Serialize
    [SerializeField] private string _androidGameID = "5174533";
    [SerializeField] private string _iOSGameID = "5174532";
    [SerializeField] private bool _testMode = true;

    // Private
    private string _gameID;

    private void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
        _gameID = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOSGameID : _androidGameID;
        Advertisement.Initialize(_gameID, _testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads Initialization Complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}
