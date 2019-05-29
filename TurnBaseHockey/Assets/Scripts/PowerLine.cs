using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerLine : MonoBehaviour
{
    public SpriteRenderer[] sprites;

    public void SetDisplay(int i)
    {
        for(int x =0;x<sprites.Length;x++)
        {
            if(x<i)
            {
                sprites[x].enabled = true;
            }
            else
            {
                sprites[x].enabled = false;
            }
        }
    }
}
