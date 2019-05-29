using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<Player> teamOne;
    public List<Player> teamTwo;

    public GameObject goalSound;

    public static Player shooter;

    public static int redPoints;
    public static int bluePoints;

    int currPlayer = 0;
    bool bothTeamPlayed = false;
    bool teamTwoStarting = false;

    private void Start()
    {
        teamOne[currPlayer].StartTurn();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void NextTurn()
    {
        if(currPlayer < teamOne.Count || currPlayer < teamTwo.Count)
        {
            if(teamTwoStarting == false)
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
            else
            {
                if (bothTeamPlayed == false)
                {
                    teamOne[currPlayer].StartTurn();
                    bothTeamPlayed = true;
                    currPlayer++;
                }
                else
                {
                    teamTwo[currPlayer].StartTurn();
                    bothTeamPlayed = false;
                }
            }

        }
        else
        {
            StartCoroutine(ExecuteTurn());
            currPlayer = 0;
        }
    }

    IEnumerator ExecuteTurn()
    {
        int i = 0;
        while(i < teamOne.Count)
        {
            if(teamTwoStarting == false)
            {
                yield return new WaitForSeconds(.25f);

                teamOne[i].Move();

                yield return new WaitForSeconds(.25f);

                teamTwo[i].Move();
            }
            else
            {
                yield return new WaitForSeconds(.25f);

                teamTwo[i].Move();

                yield return new WaitForSeconds(.25f);

                teamOne[i].Move();
            }
    

            i++;
        }

        if(shooter != null)
        {
            shooter.Shoot();
        }
      

        yield return new WaitForSeconds(3f);

        teamTwoStarting = !teamTwoStarting;
        NextTurn();
    }

    public void Score(Teams addPoint)
    {
        Instantiate(goalSound, transform.position, Quaternion.identity);
        if(addPoint == Teams.Red)
        {
            redPoints++;
        }
        else if(addPoint == Teams.Blue)
        {
            bluePoints++;
        }

        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
