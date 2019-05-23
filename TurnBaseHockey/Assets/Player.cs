using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Camera mainCam;
    Rigidbody2D _RB;

    public int throwForce;

    public int power;
    public Transform powerLine;

    bool playing = false;

    PowerLine _PL;

    Vector2 direction;

    private void Awake()
    {
        _RB = GetComponent<Rigidbody2D>();
        _PL = powerLine.GetComponent<PowerLine>();
        mainCam = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        if(playing == true)
        {
            direction = mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            direction.Normalize();

            _PL.SetDisplay(power);

            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                EndTurn();
            }

            if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
            {
                power--;
            }
            else if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
            {
                power++;
            }

            power = Mathf.Clamp(power, 1, 3);

            powerLine.rotation = Quaternion.LookRotation(Vector3.forward, new Vector2(-direction.y, direction.x));
            powerLine.Rotate(new Vector3(0, 0, -90));
        }
    }

    public void StartTurn()
    {
        playing = true;
    }

    public void EndTurn()
    {
        playing = false;
        FindObjectOfType<GameManager>().NextTurn();
    }

    public void Move()
    {
        _RB.AddForce(direction * throwForce * power, ForceMode2D.Impulse);
    }
}
