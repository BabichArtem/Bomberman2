using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MovingObject
{

    public GameObject bomb;

    [SerializeField] private float bombRate = 0.5f;
    private static float PlayerSpeed { get; set; } = 2.0f;
    private static int BombCount { get; set; } = 1;
    private static float BombDamageDistance { get; set; } = 1;

    private static bool WallWalking { get; set; } = false;


    private int bombSpawnedCount = 0;
    private float nextBomb;

    private Transform bombsHolder;



    void Start()
    {
        bombsHolder = new GameObject("Bombs").transform;
        ObjectSpeed = PlayerSpeed;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            Side direction = Side.Idle;
            if (Input.GetKey(KeyCode.UpArrow))
                direction = Side.Up;
            else if (Input.GetKey(KeyCode.DownArrow))
                direction = Side.Down;
            else if (Input.GetKey(KeyCode.LeftArrow))
                direction = Side.Left;
            else if (Input.GetKey(KeyCode.RightArrow))
                direction = Side.Right;

            if (Input.GetKey(KeyCode.Space))
            {
               SpawnBomb();
            }

            if (direction != Side.Idle)
            {
                AttempMove(gameObject, direction);
            }

        }
    }
    

    private void SpawnBomb()
    {
        if (Time.time>nextBomb)
        {
            nextBomb = Time.time + bombRate;
            Vector3 position = GetPosition(gameObject);
            GameObject instance = Instantiate(bomb, position, Quaternion.identity);
            instance.transform.SetParent(bombsHolder);
        }
    }




    public static void GetPowerUp(PowerUp.PowerUpType powerUpType)
    {
        switch (powerUpType)
        {
            case PowerUp.PowerUpType.BombCount:
                BombCount += 1;
                break;
            case PowerUp.PowerUpType.Speed:
                PlayerSpeed += 1;
                break;
            case PowerUp.PowerUpType.BombDistance:
                BombDamageDistance += 1;
                break;
            case PowerUp.PowerUpType.WallWalking:
                WallWalking = true;
                break;
         
            default:
                break;
        }
    }

}
   





