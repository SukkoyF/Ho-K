using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    bool canBeCaught = false;

    private void Start()
    {
        Invoke("SetCaught", .15f);
    }

    void SetCaught()
    {
        canBeCaught = true;
        transform.parent.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(canBeCaught == true)
        {
            Player _player = collision.GetComponent<Player>();

            if (_player != null)
            {
                GameManager.shooter = _player;
                _player.CaughtPuck();
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
