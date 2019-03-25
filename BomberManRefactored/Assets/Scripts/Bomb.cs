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
        if (DamageDistance > 1.0f)
        {
            gameObject.GetComponentInChildren<MeshRenderer>().material.color = new Color(0.2f, 0.1f, .5f, 0.1f);
        }

        StartCoroutine(ActivateBomb());
    }

    private void CheckAllCollisions()
    {
        Vector3[] sides = {Vector3.forward, Vector3.back, Vector3.left, Vector3.right};
        RaycastHit hit;
        for (int i = 0; i < 4; i++)
        {
            if (CheckSideCollision(sides[i], out hit))
            {
                CheckTagAndDestroy(hit.transform.gameObject);
            }
        }

    }

    bool CheckSideCollision(Vector3 side, out RaycastHit hit)
    {
        return Physics.Raycast(transform.position, transform.TransformDirection(side), out hit, DamageDistance);
    }

   
    

    private IEnumerator ActivateBomb()
    {
        yield return new WaitForSeconds(bombTimer);
        explosionEffect.SetActive(true);
        this.gameObject.GetComponentInChildren<Renderer>().enabled = false;
        CheckAllCollisions();
        bombDestroyed = true;
        yield return new WaitForSeconds(bombTimer);
        Destroy(this.gameObject);
    }


    private void DestroyCollapsingWall(GameObject wall)
    {
        BoardManager boardManager = GameObject.Find("GameManager").GetComponent<BoardManager>();
        boardManager.DestroyCollapsingWall((int)wall.transform.position.x, (int)wall.transform.position.z);
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

    private void DestroyPlayer(GameObject player)
    {
        player.GetComponentInParent<Player>().PlayerDeath();
    }

    private void DestroyEnemy(GameObject enemy)
    {
        Destroy(enemy);
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
}