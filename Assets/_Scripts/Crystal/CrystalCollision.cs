using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalCollision : MonoBehaviour
{
    // Serialize
    [SerializeField] private string _tagToCompare;
    [SerializeField] private LayerMask _crystalLayerMask;

    // Private
    private Rigidbody2D _rigidbody2D;

    private GameObject _gameObjectLeft;
    private GameObject _gameObjectRight;
    private RaycastHit2D _raycastHitRight;
    private RaycastHit2D _raycastHitLeft;
    private float _extraDistance = 0.2f;
    private BoxCollider2D _collider2D;
    private Color _rayColorRight = Color.black;
    private Color _rayColorLeft = Color.black;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals(_tagToCompare) && _rigidbody2D.velocity.y > 0)
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        _rigidbody2D.velocity = Vector2.zero;
    }

    private void Update()
    {
        //Debug.Log(gameObject.name + ": " + _rigidbody2D.velocity.y);

        _raycastHitRight = Physics2D.Raycast(_collider2D.bounds.center, Vector2.right, _collider2D.bounds.extents.x + _extraDistance, _crystalLayerMask);

        if (_raycastHitRight.collider != null)
        {
            _rayColorRight = Color.red;
            _gameObjectRight = _raycastHitRight.collider.gameObject;
        }
        else
        {
            _rayColorRight = Color.black;
        }

        Debug.DrawRay(_collider2D.bounds.center, Vector2.right * (_collider2D.bounds.extents.x + _extraDistance), _rayColorRight);

        _raycastHitLeft = Physics2D.Raycast(_collider2D.bounds.center, Vector2.left, _collider2D.bounds.extents.x + _extraDistance, _crystalLayerMask);

        if (_raycastHitLeft.collider != null)
        {
            _rayColorLeft = Color.red;
            _gameObjectLeft = _raycastHitLeft.collider.gameObject;
        }
        else
        {
            _rayColorLeft = Color.black;
        }

        Debug.DrawRay(_collider2D.bounds.center, Vector2.left * (_collider2D.bounds.extents.x + _extraDistance), _rayColorLeft);
    }

    private void OnDestroy()
    {
        if (_gameObjectLeft != null)
        {
            Destroy(_gameObjectLeft);
        }

        if (_gameObjectRight != null)
        {
            Destroy(_gameObjectRight);
        }
    }
}
