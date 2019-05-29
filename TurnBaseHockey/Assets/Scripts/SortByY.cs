using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortByY : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }
}
