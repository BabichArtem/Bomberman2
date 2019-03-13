using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject FloorPrefab;
    public GameObject StaticWallsPrefab;
    public GameObject CollapsingWallsPrefab;
    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;


    public int xSize = 17;
    public int zSize = 17;

    public static int[,] field;

    private int collapsingWallsCount = 10;

    private float floorHeight = 0.0f;
    private float ObjectsHeight = 0.4f;

    private Transform boardHolder;


    private List<Vector3> freeCells;


    public void SetupScene()
    {
        field = new int[xSize, zSize];
        boardHolder = new GameObject("Board").transform;
        LayoutStaticObjects();
        InstatiateFreeCells();
        LayoutWallAtRandom(CollapsingWallsPrefab, collapsingWallsCount);
        LayoutPlayer();
        LayoutEnemy();

    }


    private void LayoutStaticObjects()
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {

                Vector3 position = new Vector3(x,ObjectsHeight,z);
                GameObject instance;
                if ((x == 0 || x == xSize - 1 || z == 0 || z == zSize - 1)||(x % 2 == 0 && z % 2 == 0))
                {
                   instance = Instantiate(StaticWallsPrefab, position, Quaternion.identity);
                   field[x, z] = 1;
                }
                else
                {
                    position.y = floorHeight;
                    instance = Instantiate(FloorPrefab, position, Quaternion.identity);
                    field[x, z] = 0;
                }
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    private void InstatiateFreeCells()
    {
        freeCells = new List<Vector3>();
        for (int x = 1; x < xSize - 1; x++)
        {
            for (int z = 1; z < zSize - 1; z++)
            {
                if (!(x % 2 == 0 && z % 2 == 0))
                {
                    Vector3 freeCell = new Vector3(x,ObjectsHeight,z);
                    freeCells.Add(freeCell);
                }
            }
        }
    }

    Vector3 RandomPosition()
    {
        int index = Random.Range(0, freeCells.Count);
        Vector3 randomPosition = freeCells[index];
        freeCells.RemoveAt(index);
        return randomPosition;
    }

    void LayoutWallAtRandom(GameObject gameObject, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 position = RandomPosition();
            GameObject instance = Instantiate(gameObject, position, Quaternion.identity);
            instance.transform.SetParent(boardHolder);
            field[(int)position.x, (int)position.z] = 1;

        }
    }

    void LayoutPlayer()
    {
        Vector3 position = RandomPosition();
        Instantiate(PlayerPrefab, position, Quaternion.identity);
    }

    void LayoutEnemy()
    {
        Vector3 position = RandomPosition();
        Instantiate(EnemyPrefab, position, Quaternion.identity);
    }





}
