using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    // Public
    public static event Action OnSpawnObject;

    // Serialize
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private List<GameObject> _objects;
    [SerializeField] private float _timeBeforeSpawn = 0.5f;

    // Private
    private int _randomIndex;
    private bool _isCoroutineEnd = true;

    private void OnEnable()
    {
        ObjectController.OnGenerateNewRandomObject += SetRandomIndex;
    }

    private void OnDisable()
    {
        ObjectController.OnGenerateNewRandomObject -= SetRandomIndex;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && _isCoroutineEnd)
        {
            Instantiate(_objects[_randomIndex], _spawnPoint.position, Quaternion.identity);
            OnSpawnObject?.Invoke();

            _isCoroutineEnd = false;
            StartCoroutine(WaitBeforeSpawn());
        }
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
