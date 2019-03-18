using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionEffect;
    private float bombTimer = 2.0f;    

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

    bool CheckSideCollision(Vector3 side, out RaycastHit hit )
    {        
        return Physics.Raycast(transform.position, transform.TransformDirection(side), out hit, 1.0f);
    }

    private void CheckTagAndDestroy(GameObject obj)
    {
        if (obj.tag == "Player" || obj.tag == "Enemy" || obj.tag == "CollapsingWall")
        {
            Destroy(obj);
        }
    }


    private IEnumerator ActivateBomb()
    {
        yield return new WaitForSeconds(bombTimer);
        //GameObject exploded = Instantiate(explosionEffect, this.transform.position, Quaternion.identity);
        CheckAllCollisions();

       // Destroy(exploded);
        Destroy(this.gameObject);

    }
}