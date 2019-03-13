using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionEffect;
    void Start()
    {
        StartCoroutine(ActivateBomb(2.0f));
    }
    IEnumerator ActivateBomb(float timeToDestroy)
    {
        yield return new WaitForSeconds(timeToDestroy);
        StartCoroutine(ExplodeBomb(2.0f));
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(this.gameObject);

    }

    private IEnumerator ExplodeBomb(float timeToDestroy)
    {
        GameObject exploded = Instantiate(explosionEffect, this.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(exploded);

    }
}
