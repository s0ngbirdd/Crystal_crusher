using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    // Serialize
    [SerializeField] private LayerMask _crystalLayerMask;
    [SerializeField] private LayerMask _toCompareLayerMask;
    [SerializeField] private string _soundName;

    // Private
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
    private bool _isLastDisable;
    private ParticlePool _particlePool;

    private void Awake()
    {
        _collider2D = GetComponent<BoxCollider2D>();

        _isFirstDisable = true;
    }

    private void OnEnable()
    {
        UIController.OnRestart += EnableLastDisable;
    }

    private void Start()
    {
        _scoreController = FindObjectOfType<ScoreController>();
        _particlePool = FindObjectOfType<ParticlePool>();
    }

    private void Update()
    {
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

    private void OnDisable()
    {
        // ????????????????????????? move to another place?
        if (_isFirstDisable)
        {
            _isFirstDisable = false;
            Debug.Log("FIRST DISABLE");
        }
        else if (_isLastDisable)
        {
            //
        }
        else
        {
            _scoreController.IncreaseScore();

            _particlePool.CreateParticle(transform);

            if (!AudioManager.Instance.ReturnAudioSource(_soundName).isPlaying)
            {
                AudioManager.Instance.PlayOneShot(_soundName);
            }
        }
        // ????????????????????????

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

        UIController.OnRestart -= EnableLastDisable;
    }

    private void EnableLastDisable()
    {
        _isLastDisable = true;
    }
}
