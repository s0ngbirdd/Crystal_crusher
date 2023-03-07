using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Public
    public static event Action OnStart;

    // Serialize
    [SerializeField] private float _delayTime = 2f;
    [SerializeField] private GameObject _gameEndPopap;
    [SerializeField] private GameObject _hintPopap;
    [SerializeField] private GameObject _quitPopap;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _hintButton;
    [SerializeField] private Button _quitButton;

    // Private
    private int _spawnBlocked = 0;
    private bool _canRestart = true;
    private Coroutine _coroutine;
    private bool _isPaused;
    //private Animator _gameEndPopupAnimator;
    private Animator _hintPopupAnimator;
    private Animator _quitPopupAnimator;

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

    private void Start()
    {
        //_gameEndPopupAnimator = _gameEndPopap.GetComponent<Animator>();
        _hintPopupAnimator = _hintPopap.GetComponent<Animator>();
        _quitPopupAnimator = _quitPopap.GetComponent<Animator>();

        OnStart?.Invoke();
    }

    private void Update()
    {
        if (_spawnBlocked >= 5 && _canRestart)
        {
            _canRestart = false;
            _coroutine = StartCoroutine(WaitForDelay());
        }
    }

    private void IncreaseSpawnBlocked()
    {
        _spawnBlocked++;
    }

    private void DecreaseSpawnBlocked()
    {
        _spawnBlocked--;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        
        _canRestart = true;
    }

    private IEnumerator WaitForDelay()
    {
        yield return new WaitForSeconds(_delayTime);

        EnableGameEndPopap();
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

    /*public void CloseGameEndPopap()
    {
        _gameEndPopupAnimator.SetTrigger("Disabled");
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
    }*/

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

    public void CloseHintPopap()
    {
        _hintPopupAnimator.SetTrigger("Disabled");
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

    public void EnableQuitPopap()
    {
        _quitPopap.SetActive(true);
        _isPaused = true;
        Time.timeScale = 0;

        _soundButton.interactable = false;
        _restartButton.interactable = false;
        _hintButton.interactable = false;
        _quitButton.interactable = false;
    }

    public void CloseQuitPopap()
    {
        _quitPopupAnimator.SetTrigger("Disabled");
    }

    public void DisableQuitPopap()
    {
        _quitPopap.SetActive(false);
        _isPaused = false;
        Time.timeScale = 1;

        _soundButton.interactable = true;
        _restartButton.interactable = true;
        _hintButton.interactable = true;
        _quitButton.interactable = true;
    }

    public void PauseGame()
    {
        _isPaused = true;
        Time.timeScale = 0;

        _soundButton.interactable = false;
        _restartButton.interactable = false;
        _hintButton.interactable = false;
        _quitButton.interactable = false;
    }

    public void UnpauseGame()
    {
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
