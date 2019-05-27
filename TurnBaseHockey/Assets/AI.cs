using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : InputSource
{
    public Transform enemyGoal;
    public Transform allyGoal;

    Transform puck;

    bool turnOver = false;

    List<Player> teammates;

    private void Awake()
    {
        teammates = new List<Player>();

        foreach(Player p in transform.parent.GetComponentsInChildren<Player>())
        {
            teammates.Add(p);
        }
    }

    public override Vector2 GetDirection()
    {
        if(FindObjectOfType<Puck>())
        {
            if(Vector2.Distance(FindObjectOfType<Puck>().transform.position,transform.position) < 3f)
            {
                return FindObjectOfType<Puck>().transform.position - transform.position;
            }
            
        }
        if(IsClosestToNet())
        {
            return allyGoal.position - transform.position;
        }

        if(GameManager.shooter != null)
        {
            if(teammates.Contains(GameManager.shooter))
            {
                return (new Vector2(enemyGoal.position.x + Random.Range(0f,2f),enemyGoal.position.y + Random.Range(-3,3))  - (Vector2)transform.position);
            }

            if(GameManager.shooter.gameObject == gameObject)
            {
                return enemyGoal.position - transform.position;
            }
        }

        return puck.position - transform.position;
    }

    public override Vector2 GetShotDirection()
    {
        if(IsClosestToNet())
        {
            return GetDirectionToAlly();
        }
        else
        {
            return (new Vector2( enemyGoal.position.x,enemyGoal.position.y + Random.Range(-3,3)) - (Vector2)transform.position);
        }
    }

    Vector2 GetDirectionToAlly()
    {
        Player target = GetComponent<Player>();

        while(target == GetComponent<Player>())
        {
            target = teammates[Random.Range(0, teammates.Count)];
        }

        return target.transform.position - transform.position;
    }

    bool IsClosestToNet()
    {
        Player closestToNet = null;

        foreach (Player p in teammates)
        {
            if (closestToNet == null)
            {
                closestToNet = p;
            }

            if (Vector2.Distance(p.transform.position, allyGoal.position) < Vector2.Distance(closestToNet.transform.position, allyGoal.position))
            {
                closestToNet = p;
            }
        }

        if (closestToNet == GetComponent<Player>())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override bool ListenForTurnEnd()
    {
        return turnOver;
    }

    public override int AdjustPower()
    {
        GetComponent<Player>().SetPower(Random.Range(1, 4));
        return 0;
    }

    public override void TurnStarted()
    {
        turnOver = false;

        if(FindObjectOfType<Puck>())
        {
            puck = FindObjectOfType<Puck>().transform;
        }

        if(puck == null)
        {
            puck = GameManager.shooter.transform;
        }

        StartCoroutine(FinishTurn());
    }

    IEnumerator FinishTurn()
    {
        yield return new WaitForSeconds(.15f);

        turnOver = true;
    }
}
