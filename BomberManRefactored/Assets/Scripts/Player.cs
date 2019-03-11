using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MovingObject
{
    private GameObject player;

    public GameObject bomb;

    private Transform bombsHolder;

    private KeyCode[] EventKeys =
    {
        KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.Space
    };


    void Start()
    {
        player = this.gameObject;
        boxCollider = GetComponent<BoxCollider>();
        bombsHolder = new GameObject("Bombs").transform;
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
                Vector3 position = GetPosition(player);
                
                GameObject instance = Instantiate(bomb, position, Quaternion.identity);
                instance.transform.SetParent(bombsHolder);
            }

            bool playerMoved = AttempMove(player, direction);
            
        }
    }
}
   





