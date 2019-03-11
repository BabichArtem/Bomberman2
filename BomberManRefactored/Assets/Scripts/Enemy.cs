using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MovingObject
{

    private GameObject enemy;
    
    private Vector3 playerPosition;

    void Start()
    {
        enemy = this.gameObject;
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        MoveEnemy();
    }

    public Side RandomSide()
    {
        Array values = Enum.GetValues(typeof(Side));
        Side randomSide = (Side)values.GetValue(Random.Range(0, values.Length));
        return randomSide;
    }

    public void MoveEnemy()
    {
        //Change path length
        Side randomSide = RandomSide();
        AttempMove(enemy, randomSide);
    }

    void GetPlayerPosition()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    }



}
