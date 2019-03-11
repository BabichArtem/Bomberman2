using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MovingObject
{
    private GameObject player;


    private KeyCode[] EventKeys =
    {
        KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.Space
    };


    void Start()
    {
        player = this.gameObject;
        boxCollider = GetComponent<BoxCollider>();
    }


    // Update is called once per frame
    void Update()
    {



        if (!GameManager.PlayerMoved)
        {
            if (Input.anyKey)
            {
                Side direction = Side.Up;
                if (Input.GetKey(KeyCode.UpArrow))
                    direction = Side.Up;
                else if (Input.GetKey(KeyCode.DownArrow))
                    direction = Side.Down;
                else if (Input.GetKey(KeyCode.LeftArrow))
                    direction = Side.Left;
                else if (Input.GetKey(KeyCode.RightArrow))
                    direction = Side.Right;

                bool playerMoved = AttempMove(player, direction);
                if (playerMoved)
                    GameManager.PlayerMoved = true;

            }
        }

    }
}

   





