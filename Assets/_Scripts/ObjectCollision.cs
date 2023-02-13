using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    // Serialize
    [SerializeField] private LayerMask _crystalLayerMask;
    [SerializeField] private LayerMask _toCompareLayerMask;
    [SerializeField] private string _soundName;
    //[SerializeField] private ParticleSystem _particleSystem;
    //[SerializeField] private ScoreController _scoreController;


    // Private
    private Rigidbody2D _rigidbody2D;

    private GameObject _gameObjectLeft;
    private GameObject _gameObjectRight;
    private RaycastHit2D _raycastHitRight;
    private RaycastHit2D _raycastHitLeft;

    private RaycastHit2D _raycastHitUp;

    private float _extraDistance = 0.2f;

    private float _extraDistanceUp = 0.02f;

    private BoxCollider2D _collider2D;
    private Color _rayColorRight = Color.black;
    private Color _rayColorLeft = Color.black;

    private Color _rayColorUp = Color.black;

    private ScoreController _scoreController;

    private bool _isFirstDisable;

    private ParticlePool _particlePool;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<BoxCollider2D>();

        _isFirstDisable = true;
    }

    private void Start()
    {
        _scoreController = FindObjectOfType<ScoreController>();
        _particlePool = FindObjectOfType<ParticlePool>();
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals(_tagToCompare) && _rigidbody2D.velocity.y > 0)
        {
            Destroy(collision.gameObject);
        }
    }*/

    /*private void OnCollisionStay2D(Collision2D collision)
    {
        _rigidbody2D.velocity = Vector2.zero;
    }*/

    private void Update()
    {
        /*_raycastHitRight = Physics2D.Raycast(_collider2D.bounds.center, Vector2.right, _collider2D.bounds.extents.x + _extraDistance, _crystalLayerMask);

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




        _raycastHitUp = Physics2D.Raycast(_collider2D.bounds.center, Vector2.up, _collider2D.bounds.extents.x + _extraDistanceUp, _toCompareLayerMask);

        if (_raycastHitUp.collider != null)
        {
            _rayColorUp = Color.red;
            _rigidbody2D.velocity = Vector2.zero;
            Destroy(_raycastHitUp.collider.gameObject);
        }
        else
        {
            _rayColorUp = Color.black;
        }

        Debug.DrawRay(_collider2D.bounds.center, Vector2.up * (_collider2D.bounds.extents.x + _extraDistanceUp), _rayColorUp);*/

        _raycastHitRight = Physics2D.Raycast(_collider2D.bounds.center, Vector2.right, _collider2D.bounds.extents.x + _extraDistance, _crystalLayerMask);
        _raycastHitLeft = Physics2D.Raycast(_collider2D.bounds.center, Vector2.left, _collider2D.bounds.extents.x + _extraDistance, _crystalLayerMask);
        _raycastHitUp = Physics2D.Raycast(_collider2D.bounds.center, Vector2.up, _collider2D.bounds.extents.x + _extraDistanceUp, _toCompareLayerMask);



        if (_raycastHitRight.collider != null)
        {
            _rayColorRight = Color.red;
            _gameObjectRight = _raycastHitRight.collider.gameObject;
        }
        else
        {
            _rayColorRight = Color.black;
            _gameObjectRight = null;
        }



        if (_raycastHitLeft.collider != null)
        {
            _rayColorLeft = Color.red;
            _gameObjectLeft = _raycastHitLeft.collider.gameObject;
        }
        else
        {
            _rayColorLeft = Color.black;
            _gameObjectLeft = null;
        }



        if (_raycastHitUp.collider != null)
        {
            _rayColorUp = Color.red;
            //_rigidbody2D.velocity = Vector2.zero;

            //Destroy(_raycastHitUp.collider.gameObject);
            _raycastHitUp.collider.gameObject.SetActive(false);
        }
        else
        {
            _rayColorUp = Color.black;
        }



        Debug.DrawRay(_collider2D.bounds.center, Vector2.right * (_collider2D.bounds.extents.x + _extraDistance), _rayColorRight);
        Debug.DrawRay(_collider2D.bounds.center, Vector2.left * (_collider2D.bounds.extents.x + _extraDistance), _rayColorLeft);
        Debug.DrawRay(_collider2D.bounds.center, Vector2.up * (_collider2D.bounds.extents.x + _extraDistanceUp), _rayColorUp);
    }



    /*private void OnDestroy()
    {
        _scoreController.IncreaseScore();

        Instantiate(_particleSystem, transform.position, Quaternion.identity);


        if (_gameObjectLeft != null)
        {
            //_scoreController.IncreaseScore();
            Destroy(_gameObjectLeft);
        }

        if (_gameObjectRight != null)
        {
            //_scoreController.IncreaseScore();
            Destroy(_gameObjectRight);
        }
    }*/

    private void OnDisable()
    {
        //_rigidbody2D.velocity = Vector2.zero;

        // ????????????????????????? move to another place?
        if (_isFirstDisable)
        {
            _isFirstDisable = false;
            Debug.Log("FIRST DISABLE");
        }
        else
        {
            _scoreController.IncreaseScore();

            //Instantiate(_particleSystem, transform.position, Quaternion.identity);

            _particlePool.CreateParticle(transform);

            if (!AudioManager.Instance.ReturnAudioSource(_soundName).isPlaying)
            {
                AudioManager.Instance.PlayOneShot(_soundName);
            }
        }
        // ????????????????????????

        //Instantiate(_particleSystem, transform.position, Quaternion.identity);


        if (_gameObjectLeft != null)
        {
            //_scoreController.IncreaseScore();

            _gameObjectLeft.SetActive(false);
        }

        if (_gameObjectRight != null)
        {
            //_scoreController.IncreaseScore();

            _gameObjectRight.SetActive(false);
        }

        // to avoid collisions after enabling
        transform.position = new Vector2(transform.position.x, -4.5f);
    }
}
