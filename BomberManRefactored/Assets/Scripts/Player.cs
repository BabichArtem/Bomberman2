using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MovingObject
{

    public GameObject BombPrefab;
    private Animator animator;

    BoardManager boardManager;
    
    private UI UIScript;

    [SerializeField] private float bombRate = 0.5f;
    private float PlayerSpeed { get; set; } = 2.0f;
    private int BombCount { get; set; } = 1;
    private float BombDamageDistance { get; set; } = 1;
    private static bool WallWalking { get; set; } = false;
    GameObject trail;
    private float nextBombTime;
    private List<Bomb> bombs;

    private Transform bombsHolder;





    void Start()
    {
        //   trail = GameObject.Find("Trail");
        // trail.SetActive(false);
        animator = GetComponent<Animator>();
        boardManager = GameObject.Find("GameManager").GetComponent<BoardManager>();
        UIScript = GameObject.Find("Canvas").GetComponent<UI>();
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

        if(stepFinished)
        {
            animator.SetBool("PlayerAnimationRun", false);
        }
        else
        {
            animator.SetBool("PlayerAnimationRun", true);
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
        if (Time.time>nextBombTime && bombs.Count<BombCount)
        {
            nextBombTime = Time.time + bombRate;
            Vector3 position = GetPosition(gameObject);
            position.x = Mathf.Round(position.x);
            position.z = Mathf.Round(position.z);
            GameObject instance = Instantiate(BombPrefab, position, Quaternion.identity);
            instance.transform.SetParent(bombsHolder);
            Bomb bombScript = instance.gameObject.GetComponent<Bomb>();
            bombScript.DamageDistance = BombDamageDistance;            
            bombs.Add(bombScript);
            animator.SetTrigger("BombSet");
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
                UIScript.ChangeText(1,BombCount.ToString());
                break;
            case PowerUp.PowerUpType.Speed:
                PlayerSpeed = 4;
                UIScript.ChangeText(0, PlayerSpeed.ToString());
              //  trail.SetActive(true);
                break;
            case PowerUp.PowerUpType.BombDistance:
                BombDamageDistance += 1;
                UIScript.ChangeText(2, BombDamageDistance.ToString());
                
                break;
            case PowerUp.PowerUpType.WallWalking:
                WallWalking = true;                
                UIScript.ChangeText(3, "");
                boardManager.ChangeCollapsingWallTransparency();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(poweUpType), poweUpType, null);
        }
        
    }    
   
    public void PlayerDeath()
    {
        animator.SetTrigger("PlayerDeath");
    }
  
}
   





