using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Public
    public static event Action OnRestart;

    // Serialize
    [SerializeField] private Image _soundImage;
    [SerializeField] private Sprite _soundEnabled;
    [SerializeField] private Sprite _soundDisabled;
    //[SerializeField] private GameObject _popup;
    [SerializeField] private string _loadSceneName;

    // Private
    private ScoreController _scoreController;

    private void Start()
    {
        _scoreController = FindObjectOfType<ScoreController>();

        if (AudioManager.Instance.ReturnSoundEnabled())
        {
            _soundImage.sprite = _soundEnabled;
        }
        else if (!AudioManager.Instance.ReturnSoundEnabled())
        {
            _soundImage.sprite = _soundDisabled;
        }
    }

    public void PlaySound()
    {
        if (!AudioManager.Instance.ReturnAudioSource("Click").isPlaying)
        {
            AudioManager.Instance.PlayOneShot("Click");
        }
    }


    public void EnableDisableSound()
    {
        AudioManager.Instance.EnableDisableSoundVolume();

        if (AudioManager.Instance.ReturnSoundEnabled())
        {
            _soundImage.sprite = _soundEnabled;
        }
        else if (!AudioManager.Instance.ReturnSoundEnabled())
        {
            _soundImage.sprite = _soundDisabled;
        }
    }

    public void RestartGame()
    {
        if (_scoreController.ReturnScore() > SaveLoadSystem.Instance.LoadGame())
        {
            _scoreController.SaveScore();
        }

        OnRestart?.Invoke();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    /*public void ShowHint()
    {
        _popup.SetActive(true);
        
        // make game pause
        //Time.timeScale = 0;
    }

    public void HideHint()
    {
        _popup.SetActive(false);

        // make game unpause
        //Time.timeScale = 1;
    }*/

    public void LoadScene()
    {
        if (_scoreController.ReturnScore() > SaveLoadSystem.Instance.LoadGame())
        {
            _scoreController.SaveScore();
        }

        OnRestart?.Invoke();

        SceneManager.LoadScene(_loadSceneName);
        Time.timeScale = 1;
    }

    private void OnApplicationQuit()
    {
        if (_scoreController.ReturnScore() > SaveLoadSystem.Instance.LoadGame())
        {
            _scoreController.SaveScore();
        }
    }
}
