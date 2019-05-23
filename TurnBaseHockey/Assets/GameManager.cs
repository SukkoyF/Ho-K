using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Player> teamOne;
    public List<Player> teamTwo;

    public Player shooter;

    int currPlayer = 0;
    bool bothTeamPlayed = false;

    private void Start()
    {
        teamOne[currPlayer].StartTurn();
    }

    public void NextTurn()
    {
        if(currPlayer < teamOne.Count || currPlayer < teamTwo.Count)
        {
            if (bothTeamPlayed == false)
            {
                teamTwo[currPlayer].StartTurn();
                bothTeamPlayed = true;
                currPlayer++;
            }
            else
            {
                teamOne[currPlayer].StartTurn();
                bothTeamPlayed = false;
            }
        }
    }

    
}
