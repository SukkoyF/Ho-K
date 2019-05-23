using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Camera mainCam;
    Rigidbody2D _RB;

    public int throwForce;
    public int shotForce;

    public int power;
    public int shotPower;
    public Transform powerLine;
    public Transform shotLine;

    bool playing = false;

    PowerLine _PL;
    PowerLine _SL;

    Vector2 direction;
    Vector2 shotDirection = Vector2.zero;
    Animator _Animator;

    bool waitForPuckInput;

    private void Awake()
    {
        _RB = GetComponent<Rigidbody2D>();
        _PL = powerLine.GetComponent<PowerLine>();
        _SL = shotLine.GetComponent<PowerLine>();
        _Animator = GetComponent<Animator>();
        mainCam = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        if(playing == true)
        {
            if (waitForPuckInput == true)
            {
                shotDirection = mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                shotDirection.Normalize();

                _SL.SetDisplay(shotPower);
            }
            else
            {
                direction = mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                direction.Normalize();

                _PL.SetDisplay(power);
            }  

            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                EndTurn();
            }

            if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
            {
                if(waitForPuckInput == true)
                {
                    shotPower--;
                }
                else
                {
                    power--;
                }
               
            }
            else if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
            {
                if(waitForPuckInput == true)
                {
                    shotPower++;
                }
                else
                {
                    power++;
                }
              
            }

            power = Mathf.Clamp(power, 1, 3);
            shotPower = Mathf.Clamp(shotPower, 1, 3);

            powerLine.rotation = Quaternion.LookRotation(Vector3.forward, new Vector2(-direction.y, direction.x));
            powerLine.Rotate(new Vector3(0, 0, -90));

            shotLine.rotation = Quaternion.LookRotation(Vector3.forward, new Vector2(-shotDirection.y, shotDirection.x));
            shotLine.Rotate(new Vector3(0, 0, -90));
        }
    }

    public void StartTurn()
    {
        playing = true;
    }

    public void EndTurn()
    {
        if (GameManager.shooter == this == waitForPuckInput == false)
        {
            waitForPuckInput = true;
        }
        else
        {
            playing = false;
            waitForPuckInput = false;
            FindObjectOfType<GameManager>().Invoke("NextTurn", 1f);
        }
    }

    public void Move()
    {
        _RB.AddForce(direction * throwForce * power, ForceMode2D.Impulse);
        _PL.SetDisplay(0);
    }

    public void CaughtPuck()
    {
        _Animator.SetBool("Flash",true);
    }

    public void Shoot()
    {
        if(shotDirection != Vector2.zero)
        {
            GameObject toSpawn = Resources.Load("Puck") as GameObject;

            GameObject instance = Instantiate(toSpawn, transform.position, Quaternion.identity);

            instance.GetComponent<Rigidbody2D>().AddForce(shotDirection * shotPower * shotForce, ForceMode2D.Impulse);

            GameManager.shooter = null;
            shotDirection = Vector2.zero;
            _SL.SetDisplay(0);
            shotPower = 0;
            _Animator.SetBool("Flash", false);
        }

    }
}
