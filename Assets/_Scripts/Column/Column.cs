using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    private void Start()
    {
        Vector2 size = GetComponent<SpriteRenderer>().size;
        GetComponent<SpriteRenderer>().size = new Vector2(size.x * 1.7f * Screen.width / Screen.height, size.y);
    }
}
