using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionEffect;
    private float bombTimer = 2.0f;

    public float DamageDistance { get; set; }
    public bool bombDestroyed = false;

    void Start()
    {
        StartCoroutine(ActivateBomb());
    }

    private void CheckAllCollisions()
    {
        Vector3[] sides = {Vector3.forward, Vector3.back, Vector3.left, Vector3.right};
        RaycastHit hit;
        for (int i = 0; i < 4; i++)
        {
            if (CheckSideCollision(sides[i],out hit))
            {
                CheckTagAndDestroy(hit.transform.gameObject);
            }
        }

    }

    bool CheckSideCollision(Vector3 side, out RaycastHit hit)
    {        
        return Physics.Raycast(transform.position, transform.TransformDirection(side), out hit, DamageDistance);
    }

    private void CheckTagAndDestroy(GameObject obj)
    {
        if (obj.tag == "Player")
        {
            DestroyPlayer(obj);
        }

        if (obj.tag == "Enemy")
        {
            DestroyEnemy(obj);
        }

        if (obj.tag == "CollapsingWall")
        {
            DestroyCollapsingWall(obj);
        }

    }

    private void DestroyPlayer(GameObject player)
    {
        Destroy(player);
    }

    private void DestroyEnemy(GameObject enemy)
    {
        Destroy(enemy);
    }

    private void DestroyCollapsingWall(GameObject wall)
    {
        BoardManager boardManager = GameObject.Find("GameManager").GetComponent<BoardManager>();
        boardManager.DestroyCollapsingWall((int) wall.transform.position.x, (int) wall.transform.position.z);
        foreach (var powerUp in boardManager.PowerUpsList)
        {
            if (powerUp != null)
            {
                if (powerUp.transform.position == wall.transform.position)
                {
                    powerUp.gameObject.SetActive(true);
                }
            }
        }
        Destroy(wall);
    }

    private IEnumerator ActivateBomb()
    {
        yield return new WaitForSeconds(bombTimer);
        //GameObject exploded = Instantiate(explosionEffect, this.transform.position, Quaternion.identity);
        CheckAllCollisions();
        bombDestroyed = true;
       // Destroy(exploded);
        Destroy(this.gameObject);
    }
}