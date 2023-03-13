using UnityEngine;

public class PopupDeactivator : MonoBehaviour
{
    // Serialize
    [SerializeField] private string _popupName;

    // Private
    private GameController _gameController;

    private void Start()
    {
        _gameController = FindObjectOfType<GameController>();
    }

    public void DeactivatePopup()
    {
        if (_popupName.Equals("Hint"))
        {
            _gameController.DisableHintPopap();
        }
        else if (_popupName.Equals("Quit"))
        {
            _gameController.DisableQuitPopap();
        }
    }
}
