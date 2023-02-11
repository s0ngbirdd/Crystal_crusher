using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{
    // Public
    public static event Action<int> OnGenerate;

    // Serialize
    [SerializeField] private List<GameObject> _crystals;

    // Private
    private int randomIndex;

    private void OnEnable()
    {
        CrystalSpawner.OnSpawn += ActivateCrystal;
    }

    private void OnDisable()
    {
        CrystalSpawner.OnSpawn -= ActivateCrystal;
    }

    private void Start()
    {
        /*randomIndex = UnityEngine.Random.Range(0, _crystals.Count);
        OnGenerate?.Invoke(randomIndex);*/

        ActivateCrystal();
    }

    private void ActivateCrystal()
    {
        foreach (var crystal in _crystals)
        {
            crystal.SetActive(false);
        }

        randomIndex = UnityEngine.Random.Range(0, _crystals.Count);

        _crystals[randomIndex].SetActive(true);

        OnGenerate?.Invoke(randomIndex);
    }
}
