using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartEnemy : Enemy
{

    private void Update()
    {
        MoveEnemy();
    }

    protected override void MoveEnemy()
    {

        base.MoveEnemy();

    }
    protected override Side GetMovingSide()
    {
        Vector3 enemyPosition = GetEnemyPosition();
        Vector3 playerPosition = GetPlayerPosition();

        Side movingSide = Side.Idle;
        Vector3 nextCell = AStar.GetNextCellVector(BoardManager.field, enemyPosition, playerPosition);

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
