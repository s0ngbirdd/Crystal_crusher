using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    // Serialize
    [SerializeField] private int _poolCount = 5;
    [SerializeField] private bool _autoExpand = true;
    [SerializeField] private Particle _particlePrefab;

    // Private
    private PoolMono<Particle> _particlePool;

    private void Start()
    {
        _particlePool = new PoolMono<Particle>(_particlePrefab, _poolCount, transform);
        _particlePool.AutoExpand = _autoExpand;
    }

    public void CreateParticle(Transform objectTransform)
    {
        var particle = _particlePool.GetFreeElement();
        particle.transform.position = objectTransform.position;
    }
}
