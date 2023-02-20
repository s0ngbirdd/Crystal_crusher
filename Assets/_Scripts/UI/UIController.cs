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

    private void Start()
    {
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

    public void QuitGame()
    {
        Application.Quit();
    }
}
