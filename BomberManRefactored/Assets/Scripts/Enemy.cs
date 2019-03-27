using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MovingObject
{
    protected Animator animator;
    protected bool isActive = true;
    private float EnemySpeed = 2.0f;


    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        if (isActive)
        {
            MoveEnemy();
        }
    }

    public void OnChildTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            isActive = false;
            animator.SetTrigger("Hit");
            col.GetComponentInParent<Player>().PlayerDeath();
            //   Destroy(col.gameObject);
        }

    }

    public Side RandomSide()
    {
        Array values = Enum.GetValues(typeof(Side));
        Side randomSide = (Side) values.GetValue(Random.Range(0, values.Length));
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
            animator.SetBool("Run", true);
            AttempMove(gameObject, GetMovingSide(), EnemySpeed);
        }
    }

    public virtual void EnemyDeath()
    {
        animator.SetTrigger("Death");
        isActive = false;
        StartCoroutine(DestroyEnemy(2.0f));
    }

    protected Vector3 GetPlayerPosition()
    {
        return GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    protected Vector3 GetEnemyPosition()
    {
        return this.transform.position;
    }

    IEnumerator DestroyEnemy(float timeToDestroy)
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(this.gameObject);
    }

}