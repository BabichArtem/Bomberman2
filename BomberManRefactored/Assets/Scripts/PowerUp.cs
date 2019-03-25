using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public PowerUpType PUType;

    public enum PowerUpType
    {
        BombCount,
        Speed,
        BombDistance,
        WallWalking
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider unit)
    {
        if (unit.gameObject.tag == "Player")
        {
            var player = unit.gameObject.GetComponentInParent<Player>();
            player.ApplyPowerUp(PUType);
            Destroy(this.gameObject);
        }
        
    }

    

}
