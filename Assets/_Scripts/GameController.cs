using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Serialize
    [SerializeField] private float _delayTime = 2f;
    [SerializeField] private GameObject _gameEndPopap;
    [SerializeField] private GameObject _hintPopap;

    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _hintButton;
    [SerializeField] private Button _quitButton;

    // Private
    private int _spawnBlocked = 0;
    //private bool _isCoroutineEnd = true;
    private bool _canRestart = true;
    private Coroutine _coroutine;
    private bool _isPaused;

    private void OnEnable()
    {
        SpawnBlocker.OnSpawnBlock += IncreaseSpawnBlocked;
        SpawnBlocker.OnSpawnUnblock += DecreaseSpawnBlocked;
    }

    private void OnDisable()
    {
        SpawnBlocker.OnSpawnBlock -= IncreaseSpawnBlocked;
        SpawnBlocker.OnSpawnUnblock -= DecreaseSpawnBlocked;
    }

    private void Update()
    {
        /*if (_spawnBlocked >= 5 && _isCoroutineEnd)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }*/

        if (_spawnBlocked >= 5 && _canRestart)
        {
            _canRestart = false;
            _coroutine = StartCoroutine(WaitForDelay());
        }
    }

    private void IncreaseSpawnBlocked()
    {
        //_coroutine = StartCoroutine(WaitForDelay());
        //_isCoroutineEnd = false;

        _spawnBlocked++;
        Debug.Log("SpawnBlocked++ >>> " + _spawnBlocked);
    }

    private void DecreaseSpawnBlocked()
    {
        _spawnBlocked--;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        
        _canRestart = true;
        Debug.Log("SpawnBlocked-- >>> " + _spawnBlocked);
    }

    private IEnumerator WaitForDelay()
    {
        yield return new WaitForSeconds(_delayTime);

        EnableGameEndPopap();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        //_isCoroutineEnd = true;
    }

    public void EnableGameEndPopap()
    {
        _gameEndPopap.SetActive(true);
        _isPaused = true;
        Time.timeScale = 0;

        _soundButton.interactable = false;
        _restartButton.interactable = false;
        _hintButton.interactable = false;
        _quitButton.interactable = false;
    }

    public void DisableGameEndPopap()
    {
        _gameEndPopap.SetActive(false);
        _isPaused = false;
        Time.timeScale = 1;

        _soundButton.interactable = true;
        _restartButton.interactable = true;
        _hintButton.interactable = true;
        _quitButton.interactable = true;
    }

    public void EnableHintPopap()
    {
        _hintPopap.SetActive(true);
        _isPaused = true;
        Time.timeScale = 0;

        _soundButton.interactable = false;
        _restartButton.interactable = false;
        _hintButton.interactable = false;
        _quitButton.interactable = false;
    }

    public void DisableHintPopap()
    {
        _hintPopap.SetActive(false);
        _isPaused = false;
        Time.timeScale = 1;

        _soundButton.interactable = true;
        _restartButton.interactable = true;
        _hintButton.interactable = true;
        _quitButton.interactable = true;
    }

    public bool ReturnIsPaused()
    {
        return _isPaused;
    }
}
