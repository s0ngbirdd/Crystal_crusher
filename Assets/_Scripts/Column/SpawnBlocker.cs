using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlocker : MonoBehaviour
{
    // Public
    public static event Action OnSpawnBlock;
    public static event Action OnSpawnUnblock;

    // Serialize
    [SerializeField] private string _tagToCompare = "RockPaperPig";
    [SerializeField] private BoxCollider2D _boxCollider2D;

    // Private
    private bool _canSpawn = true;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals(_tagToCompare) && _canSpawn)
        {
            _canSpawn = false;
            Debug.Log("Can spawn >>> " + _canSpawn);
            OnSpawnBlock?.Invoke();
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals(_tagToCompare))
        {
            _canSpawn = true;
            Debug.Log("Can spawn >>> " + _canSpawn);
            OnSpawnUnblock?.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        if (_canSpawn)
        {
            Gizmos.color = Color.white;
        }
        else
        {
            Gizmos.color = Color.red;
        }

        Gizmos.DrawWireCube(transform.position + (Vector3)_boxCollider2D.offset, _boxCollider2D.size);
    }

    public bool ReturnCanSpawn()
    {
        return _canSpawn;
    }
}
