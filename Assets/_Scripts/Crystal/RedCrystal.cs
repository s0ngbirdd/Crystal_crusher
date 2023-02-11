using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCrystal : MonoBehaviour
{
    // Private
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("GreenCrystal") && _rigidbody2D.velocity.y > 0)
        {
            Destroy(collision.gameObject);
        }
    }
}
