using UnityEngine;

public class ObjectDeactivator : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.SetActive(false);
    }
}
