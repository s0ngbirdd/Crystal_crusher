using UnityEngine;

public class OrtographicSizeFitter : MonoBehaviour
{
    // Serialize
    [SerializeField] private SpriteRenderer _gameBoard;
    [SerializeField] private int _spriteRendererNumber = 1;

    // Private
    private float _ortographicSize;

    private float _screenRatio;
    private float _targetRatio;
    private float _differenceInSize;

    private void Start()
    {
        // Fit camera by width

        /*_ortographicSize = _gameBoard.size.x * Screen.height * _spriteRendererNumber / Screen.width * 0.5f;
        Camera.main.orthographicSize = _ortographicSize;*/

        // Fit camera by height

        _ortographicSize = _gameBoard.bounds.size.y / 2;
        Camera.main.orthographicSize = _ortographicSize;

        // Perfect camera fit

        /*_screenRatio = (float)Screen.width / (float)Screen.height;
        _targetRatio = _gameBoard.bounds.size.x * _spriteRendererNumber / _gameBoard.bounds.size.y;

        if (_screenRatio >= _targetRatio)
        {
            Camera.main.orthographicSize = _gameBoard.bounds.size.y / 2;
        }
        else
        {
            _differenceInSize = _targetRatio / _screenRatio;
            Camera.main.orthographicSize = _gameBoard.bounds.size.y / 2 * _differenceInSize;
        }*/
    }
}
