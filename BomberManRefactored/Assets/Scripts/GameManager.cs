using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private BoardManager boardScript;


    public static bool PlayerMoved = false; 

    void Awake()
    {
        boardScript = GetComponent<BoardManager>();

        InitGame();
    }

    void InitGame()
    {
        boardScript.SetupScene();
    }


}
