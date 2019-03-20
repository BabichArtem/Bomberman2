using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MovingObject
{
    private float EnemySpeed = 2.0f;

    void Update()
    {
        MoveEnemy();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Destroy(col.gameObject);
        }

    }

    public Side RandomSide()
    {
        Array values = Enum.GetValues(typeof(Side));
        Side randomSide = (Side)values.GetValue(Random.Range(0, values.Length));
        return randomSide;
    }

    protected virtual Side GetMovingSide()
    {
        return RandomSide();
    }

    protected virtual void MoveEnemy()
    {
        if (stepFinished)
        {
            AttempMove(gameObject, GetMovingSide(), EnemySpeed);
        }
    }

    protected Vector3 GetPlayerPosition()
    {
        return GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    protected Vector3 GetEnemyPosition()
    {
        return this.transform.position;
    }

}