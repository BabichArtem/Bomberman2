using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MovingObject
{

    private GameObject enemy;

    private List<Vector3> findedPath;
    private AStar aStar;

    void Start()
    {
        enemy = this.gameObject;
        boxCollider = GetComponent<BoxCollider>();
        aStar = new AStar();
    }

    void Update()
    {
        MoveEnemy();
    }

    public Side RandomSide()
    {
        Array values = Enum.GetValues(typeof(Side));
        Side randomSide = (Side) values.GetValue(Random.Range(0, values.Length));
        return randomSide;
    }



    public void MoveEnemy()
    {
        Vector3 enemyPosition =GetEnemyPosition();
        Vector3 playerPosition = GetPlayerPosition();
        CalculatePath(enemyPosition, playerPosition);
        Side movingSide = Side.Idle;

        if (findedPath != null)
        {
            Vector3 nextCell = findedPath[1];

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

            Debug.Log(movingSide.ToString());

            AttempMove(enemy, movingSide);
        }
        else
        {
            Debug.Log("Null");
        }
    }

    Vector3 GetPlayerPosition()
    {
        return GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    Vector3 GetEnemyPosition()
    {
        return this.transform.position;
    }

    void CalculatePath(Vector3 startPosition, Vector3 endPosition)
    {
        
        findedPath = new List<Vector3>();
        findedPath = aStar.PointToVector3(BoardManager.field, startPosition, endPosition);
    }   
}