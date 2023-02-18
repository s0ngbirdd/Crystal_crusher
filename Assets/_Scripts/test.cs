using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private Vector3 _temp;

    private void Awake()
    {
        //_temp = transform.position;
    }
    private void Start()
    {
        //_camera = Camera.main;
        _temp = transform.position;
        Vector3 newPosition = _camera.WorldToScreenPoint(_temp);
        Vector3 uiPosition = new Vector3(newPosition.x, Screen.height - newPosition.y, newPosition.z);
        transform.position = uiPosition;

        //Camera.ViewportToWorldPoint(temp);
        //float newObjectWidth = oldObjectWidth * 764 / 1368 * Screen.width / Screen.height;
    }
}
