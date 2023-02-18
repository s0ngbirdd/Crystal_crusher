using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Private
    private int _spawnBlocked = 0;
    private bool _isCoroutineEnd = true;
    private Coroutine _coroutine;

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
        if (_spawnBlocked >= 5 && _isCoroutineEnd)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void IncreaseSpawnBlocked()
    {
        _coroutine = StartCoroutine(WaitForDelay());
        _isCoroutineEnd = false;

        _spawnBlocked++;
    }

    private void DecreaseSpawnBlocked()
    {
       StopCoroutine(_coroutine);

        _spawnBlocked--;
    }

    private IEnumerator WaitForDelay()
    {
        yield return new WaitForSeconds(1f);

        _isCoroutineEnd = true;
    }
}
