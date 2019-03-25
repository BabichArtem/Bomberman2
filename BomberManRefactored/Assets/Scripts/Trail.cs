using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Trail : MonoBehaviour
{
    private Bomb bombScript;
    private BoxCollider boxCol;
    public float xPos;
    public float zPos;
    private bool Started = false;
    private float objectSpeed = 5.0f;

    void Start()
    {
        boxCol = GetComponent<BoxCollider>();

        bombScript = gameObject.GetComponentInParent<Bomb>();
        xPos *= bombScript.DamageDistance;
        zPos *= bombScript.DamageDistance;
    }

    void Update()
    {
        if (this.gameObject.activeSelf && !Started)
        {
            Vector3 startPos = new Vector3(0.0f,0.3f,0.0f);
            Vector3 endPos = new Vector3(xPos, 0.3f, zPos);
            StartCoroutine(MoveTrail(startPos, endPos));
        }

    }

    IEnumerator MoveTrail(Vector3 startPosition, Vector3 endPosition)
    {
        Started = true;
        float step = (objectSpeed / (startPosition - endPosition).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step;
            this.transform.localPosition = Vector3.Lerp(startPosition, endPosition, t);
            yield return new WaitForFixedUpdate();
        }

        this.transform.localPosition = endPosition;
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag=="StaticWall")
        {
            Destroy(this.gameObject);
        }
        

    }




}
