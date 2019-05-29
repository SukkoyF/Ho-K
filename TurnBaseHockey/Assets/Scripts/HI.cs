using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HI : InputSource
{
    Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;    
    }

    public override Vector2 GetDirection()
    {
        return mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    public override Vector2 GetShotDirection()
    {
        return GetDirection();
    }

    public override bool ListenForTurnEnd()
    {
        return Input.GetKeyDown(KeyCode.Mouse0);
    }

    public override int AdjustPower()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            return -1;
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
