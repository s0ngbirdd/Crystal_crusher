using System;
using System.Collections;
using UnityEngine;

public class MainMenuObjectPool : MonoBehaviour
{
    // Serialize
    [SerializeField] private int _poolCount = 5;
    [SerializeField] private bool _autoExpand = true;
    [SerializeField] private Rock _rockPrefab;
    [SerializeField] private Paper _paperPrefab;
    [SerializeField] private Pig _pigPrefab;
    [SerializeField] private Transform _spawnPoint;

    // Private
    private PoolMono<Rock> _rockPool;
    private PoolMono<Paper> _paperPool;
    private PoolMono<Pig> _pigPool;
    private int _randomIndex;
    private bool _isCoroutineEnd;
    private float _timeBeforeSpawn;

    private void Start()
    {
        _rockPool = new PoolMono<Rock>(_rockPrefab, _poolCount, transform);
        _rockPool.AutoExpand = _autoExpand;

        _paperPool = new PoolMono<Paper>(_paperPrefab, _poolCount, transform);
        _paperPool.AutoExpand = _autoExpand;

        _pigPool = new PoolMono<Pig>(_pigPrefab, _poolCount, transform);
        _pigPool.AutoExpand = _autoExpand;

        CreateRandomObject();
        StartCoroutine(WaitBeforeSpawn());
    }

    private void Update()
    {
        if (_isCoroutineEnd)
        {
            CreateRandomObject();

            _isCoroutineEnd = false;
            StartCoroutine(WaitBeforeSpawn());
        }
    }

    private void CreateRandomObject()
    {
        _randomIndex = UnityEngine.Random.Range(0, 3);

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

    private IEnumerator WaitBeforeSpawn()
    {
        _timeBeforeSpawn = UnityEngine.Random.Range(3, 5);

        yield return new WaitForSeconds(_timeBeforeSpawn);

        _isCoroutineEnd = true;
    }
}
