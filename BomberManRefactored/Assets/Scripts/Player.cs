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
    private float PlayerSpeed = 3.0f;

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

}
   





