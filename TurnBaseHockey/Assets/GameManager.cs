using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Player> teamOne;
    public List<Player> teamTwo;

    public Player shooter;

    private void Start()
    {
        teamOne[0].StartTurn();
    }

    public void NextTurn()
    {

    }
}
