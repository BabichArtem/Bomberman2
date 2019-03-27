using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColider : MonoBehaviour
{
    private Enemy ParentScript;
    // Start is called before the first frame update
    void Start()
    {
        ParentScript = transform.parent.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        ParentScript.OnChildTriggerEnter(col);
    }
}
