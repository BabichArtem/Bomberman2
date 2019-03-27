using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartEnemy : Enemy
{
    private BoardManager boardScript;

    protected override void Start()
    {
        boardScript = GameObject.Find("GameManager").GetComponent<BoardManager>();
        base.Start();
    }

    protected override Side GetMovingSide()
    {
        Vector3 enemyPosition = GetEnemyPosition();
        Vector3 playerPosition = GetPlayerPosition();

        Side movingSide = Side.Idle;
       
        Vector3 nextCell = AStar.GetNextCellVector(boardScript.Field, enemyPosition, playerPosition);

        if (nextCell != null)
        {
            if (nextCell.x < enemyPosition.x)
            {
                movingSide = Side.Left;
            }
            else if (nextCell.x > enemyPosition.x)
            {
                movingSide = Side.Right;
            }
            else if (nextCell.z > enemyPosition.z)
            {
                movingSide = Side.Up;
            }
            else if (nextCell.z < enemyPosition.z)
            {
                movingSide = Side.Down;
            }
        }
        
        return movingSide;
    }

}
