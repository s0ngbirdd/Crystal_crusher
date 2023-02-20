using System;
using System.Collections;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // Public
    public static event Action OnSpawnObject;

    // Serialize
    [SerializeField] private int _poolCount = 5;
    [SerializeField] private bool _autoExpand = true;
    [SerializeField] private Rock _rockPrefab;
    [SerializeField] private Paper _paperPrefab;
    [SerializeField] private Pig _pigPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _timeBeforeSpawn = 0.5f;
    [SerializeField] private SpawnBlocker _spawnBlocker;

    // Private
    private PoolMono<Rock> _rockPool;
    private PoolMono<Paper> _paperPool;
    private PoolMono<Pig> _pigPool;
    private int _randomIndex;
    private bool _isCoroutineEnd = true;
    private GameController _gameController;

    private void OnEnable()
    {
        ObjectController.OnGenerateNewRandomObject += SetRandomIndex;
    }

    private void OnDisable()
    {
        ObjectController.OnGenerateNewRandomObject -= SetRandomIndex;
    }

    private void Start()
    {
        _rockPool = new PoolMono<Rock>(_rockPrefab, _poolCount, transform);
        _rockPool.AutoExpand = _autoExpand;

        _paperPool = new PoolMono<Paper>(_paperPrefab, _poolCount, transform);
        _paperPool.AutoExpand = _autoExpand;

        _pigPool = new PoolMono<Pig>(_pigPrefab, _poolCount, transform);
        _pigPool.AutoExpand = _autoExpand;

        _gameController = FindObjectOfType<GameController>();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && _isCoroutineEnd && _spawnBlocker.ReturnCanSpawn() && !_gameController.ReturnIsPaused())
        {
            CreateRandomObject();
            OnSpawnObject?.Invoke();

            _isCoroutineEnd = false;
            StartCoroutine(WaitBeforeSpawn());

            if (!AudioManager.Instance.ReturnAudioSource("Spawn").isPlaying)
            {
                AudioManager.Instance.PlayOneShot("Spawn");
            }
        }
    }

    private void CreateRandomObject()
    {
        if (_randomIndex == 0)
        {
            CreateRock();
        }
        else if (_randomIndex == 1)
        {
            CreatePaper();
        }
        else if (_randomIndex == 2)
        {
            CreatePig();
        }
    }

    private void CreateRock()
    {
        var rock = _rockPool.GetFreeElement();
        rock.transform.position = _spawnPoint.position;
    }

    private void CreatePaper()
    {
        var paper = _paperPool.GetFreeElement();
        paper.transform.position = _spawnPoint.position;
    }

    private void CreatePig()
    {
        var pig = _pigPool.GetFreeElement();
        pig.transform.position = _spawnPoint.position;
    }

    private void SetRandomIndex(int index)
    {
        _randomIndex = index;
    }

    private IEnumerator WaitBeforeSpawn()
    {
        yield return new WaitForSeconds(_timeBeforeSpawn);

        _isCoroutineEnd = true;
    }
}
