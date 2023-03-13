using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    // Public
    public static event Action<int> OnGenerateNewRandomObject;

    // Serialize
    [SerializeField] private List<GameObject> _objects;

    // Private
    private int _randomIndex;

    private void OnEnable()
    {
        ObjectPool.OnSpawnObject += ActivateRandomObject;
    }

    private void OnDisable()
    {
        ObjectPool.OnSpawnObject -= ActivateRandomObject;
    }

    private void Start()
    {
        ActivateRandomObject();
    }

    private void ActivateRandomObject()
    {
        foreach (var obj in _objects)
        {
            obj.SetActive(false);
        }

        _randomIndex = UnityEngine.Random.Range(0, _objects.Count);
        _objects[_randomIndex].SetActive(true);
        OnGenerateNewRandomObject?.Invoke(_randomIndex);
    }
}
