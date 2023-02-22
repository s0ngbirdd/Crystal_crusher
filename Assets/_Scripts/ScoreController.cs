using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    // Serialize
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private int _showAdsRate = 10;

    // Private
    private int _score = 0;

    private void Start()
    {
        _score = SaveLoadSystem.Instance.LoadGame();
        Debug.Log("LoadScore >>> " + _score);
    }

    private void Update()
    {
        _scoreText.text = _score.ToString();
    }

    public void IncreaseScore()
    {
        _score++;

        if (_score % _showAdsRate == 0)
        {
            SaveLoadSystem.Instance.SaveGame(_score);
            Debug.Log("SaveScore >>> " + _score);

            InterstitialAds.Instance.ShowAd();
        }
    }
}
