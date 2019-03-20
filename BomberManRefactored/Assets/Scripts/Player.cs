using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MovingObject
{

    public GameObject BombPrefab;

    private List<Bomb> bombs;

    [SerializeField] private float bombRate = 0.5f;
    private float PlayerSpeed { get; set; } = 2.0f;
    private int BombCount { get; set; } = 1;
    private float BombDamageDistance { get; set; } = 1;
    private static bool WallWalking { get; set; } = false;

    private float nextBomb;

    private Transform bombsHolder;



    void Start()
    {
        bombs = new List<Bomb>();
        bombsHolder = new GameObject("Bombs").transform;
        
    }


    // Update is called once per frame
    void Update()
    {
        CheckBombs();
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
                AttempMove(gameObject, direction, PlayerSpeed);
            }

        }
    }



    protected override bool CanMove(Vector3 startPosition, Vector3 endPosition)
    {
        LayerMask mask = LayerMask.GetMask("BlockingLayer");
        Ray ray = new Ray(startPosition, endPosition - startPosition);
        RaycastHit hitInfo;
        bool detected = Physics.Raycast(ray,out hitInfo,1.0f, mask);
        if (detected)
        {
            if (hitInfo.transform.tag == "StaticWall")
            {
                return false;
            }

            if (hitInfo.transform.tag == "CollapsingWall" && WallWalking)
            {
                return true;
            }
        }
        return !detected;
    }

    private void SpawnBomb()
    {
        if (Time.time>nextBomb && bombs.Count<BombCount)
        {
            nextBomb = Time.time + bombRate;
            Vector3 position = GetPosition(gameObject);
            GameObject instance = Instantiate(BombPrefab, position, Quaternion.identity);
            instance.transform.SetParent(bombsHolder);
            Bomb bombScript = instance.gameObject.GetComponent<Bomb>();
            bombScript.DamageDistance = BombDamageDistance;
            bombs.Add(bombScript);
            
        }
    }

    private void CheckBombs()
    {
        if (bombs.Count>0)
        {
            foreach (var bomb in bombs)
            {
                if (bomb == null || bomb.bombDestroyed)
                {
                    bombs.Remove(bomb);
                }
            }
        }
    }

    public void ApplyPowerUp(PowerUp.PowerUpType poweUpType)
    {
        switch (poweUpType)
        {
            case PowerUp.PowerUpType.BombCount:
                BombCount += 1;
                break;
            case PowerUp.PowerUpType.Speed:
                PlayerSpeed += 2;
                break;
            case PowerUp.PowerUpType.BombDistance:
                BombDamageDistance += 1;
                break;
            case PowerUp.PowerUpType.WallWalking:
                WallWalking = true;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(poweUpType), poweUpType, null);
        }
    }
}
   





