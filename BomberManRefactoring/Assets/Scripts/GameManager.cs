using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private BoardManager boardScript;

    private List<Enemy> enemies;

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

    public void AddEnemyToList(Enemy script)
    {
        //Add Enemy to List enemies.
        enemies.Add(script);
    }

}
