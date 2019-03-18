using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    [SerializeField] PowerUpType PUType { get; set; }

    public enum PowerUpType
    {
        BombCount,
        Speed,
        BombDistance,
        WallWalking
    }

    private void OnTriggerEnter(Collider unit)
    {
        if (unit.tag == "Player")
        {
            Player.GetPowerUp(PUType);
        }
    }

}
