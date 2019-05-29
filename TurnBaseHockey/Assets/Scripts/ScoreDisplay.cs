using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    Text scoreDisplay;

    private void Awake()
    {
        scoreDisplay = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        scoreDisplay.text = GameManager.bluePoints + "     " + GameManager.redPoints;
    }
}
