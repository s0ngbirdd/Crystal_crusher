using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{
    // Serialize
    [SerializeField] private string _loadSceneName = "RockPaperPig";

    public void PlaySound()
    {
        if (!AudioManager.Instance.ReturnAudioSource("Click").isPlaying)
        {
            AudioManager.Instance.PlayOneShot("Click");
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(_loadSceneName);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
