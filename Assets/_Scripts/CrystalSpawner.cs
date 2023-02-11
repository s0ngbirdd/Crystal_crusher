using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSpawner : MonoBehaviour
{
    // Public
    public static event Action OnSpawn;

    // Serialize
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private List<GameObject> _crystals;

    // Private
    private int randomIndex;

    private void OnEnable()
    {
        CrystalController.OnGenerate += SetRandomIndex;
    }

    private void OnDisable()
    {
        CrystalController.OnGenerate -= SetRandomIndex;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //int randomIndex = UnityEngine.Random.Range(0, _crystals.Count);
            Instantiate(_crystals[randomIndex], _spawnPoint.position, Quaternion.identity);
            OnSpawn?.Invoke();
        }
    }

    private void SetRandomIndex(int index)
    {
        randomIndex = index;
    }
}
