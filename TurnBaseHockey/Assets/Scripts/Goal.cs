using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Teams { Red,Blue}

public class Goal : MonoBehaviour
{
    public Teams scoreTo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Puck>())
        {
            FindObjectOfType<GameManager>().Score(scoreTo);
            Destroy(collision.gameObject);
        }
    }
}
