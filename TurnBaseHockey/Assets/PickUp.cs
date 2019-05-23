using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>())
        {
            FindObjectOfType<GameManager>().shooter = collision.GetComponent<Player>();

            Destroy(this.gameObject);
        }
    }
}
