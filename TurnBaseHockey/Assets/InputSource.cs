using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSource : MonoBehaviour
{
    public virtual Vector2 GetDirection()
    {
        return Vector2.zero;
    }

    public virtual Vector2 GetShotDirection()
    {
        return Vector2.zero;
    }

    public virtual bool ListenForTurnEnd()
    {
        return true;
    }

    public virtual int AdjustPower()
    {
        return 0;
    }

    public virtual void TurnStarted()
    {

    }
}
