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

    public Transform myDisplay;

    public GameObject sfx;
    public GameObject shotSFX;

    bool playing = false;

    PowerLine _PL;
    PowerLine _SL;

    Vector2 direction;
    Vector2 shotDirection = Vector2.zero;
    Animator _Animator;

    bool waitForPuckInput;

    InputSource _InputSource;

    private void Awake()
    {
        _RB = GetComponent<Rigidbody2D>();
        _PL = powerLine.GetComponent<PowerLine>();
        _SL = shotLine.GetComponent<PowerLine>();
        _Animator = GetComponent<Animator>();
        mainCam = FindObjectOfType<Camera>();
        _InputSource = GetComponent<InputSource>();
    }

    private void Update()
    {
        if(playing == true)
        {
            if (waitForPuckInput == true)
            {
                shotDirection = _InputSource.GetShotDirection();
                shotDirection.Normalize();
            }
            else
            {
                direction = _InputSource.GetDirection();
                direction.Normalize();
            }  

            if (_InputSource.AdjustPower() < 0)
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
            else if (_InputSource.AdjustPower() > 0)
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

            if(waitForPuckInput == true)
            {
                _SL.SetDisplay(shotPower);
            }
        
            _PL.SetDisplay(power);

            if (_InputSource.ListenForTurnEnd())
            {
                EndTurn();
            }
        }
    }

    private void LateUpdate()
    {
        if(_RB.velocity.magnitude > .05f)
        {
            myDisplay.GetComponent<Animator>().SetBool("Moving", true);
        }
        else
        {
            myDisplay.GetComponent<Animator>().SetBool("Moving", false);
        }

        if(_RB.velocity.x > 0)
        {
            myDisplay.transform.localScale = new Vector3(2, 2, 1);
        }
        else if(_RB.velocity.x < 0)
        {
            myDisplay.transform.localScale = new Vector3(-2, 2, 1);
        }
    }

    public void SetPower(int i)
    {
        if (waitForPuckInput == true)
        {
            shotPower = i;
        }
        else
        {
            power = i;
        }
    }

    public void StartTurn()
    {
        playing = true;
        _InputSource.TurnStarted();
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
        Instantiate(sfx, transform.position, Quaternion.identity);

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

            Instantiate(shotSFX, transform.position, Quaternion.identity);
            GameManager.shooter = null;
            shotDirection = Vector2.zero;
            _SL.SetDisplay(0);
            shotPower = 0;
            _Animator.SetBool("Flash", false);
        }

    }
}
