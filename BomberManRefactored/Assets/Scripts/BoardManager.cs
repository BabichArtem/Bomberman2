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
    public GameObject SmartEnemyPrefab;

    public GameObject[] PowerUpPrefabs;


    [SerializeField] public int xSize = 17;
    [SerializeField] public int zSize = 17;

    public int[,] Field;

    [SerializeField] private int CollapsingWallsCount = 7;

    private float floorHeight = 0.0f;
    private float ObjectsHeight = 0.4f;

    private Transform boardHolder;


    private List<Vector3> FreeCells;
    private List<Vector3> FreeCollapsingWallsList;

    public List<PowerUp> PowerUpsList;


    public void SetupScene(int level)
    {
        PowerUpsList = new List<PowerUp>();
        FreeCollapsingWallsList = new List<Vector3>();
        Field = new int[xSize, zSize];
        boardHolder = new GameObject("Board").transform;
        LayoutStaticObjects();

        InstatiateFreeCells();
        LayoutCollapsingWalls(CollapsingWallsCount);

        LayoutPowerUp(PowerUpPrefabs[0], 1);
        LayoutPowerUp(PowerUpPrefabs[1], 1);
        LayoutPowerUp(PowerUpPrefabs[2], 1);
        LayoutPowerUp(PowerUpPrefabs[3], 1);

        LayoutPlayer();
        
        for (int i = 0; i < level; i++)
        {
            LayoutEnemy(false);
        }
        LayoutEnemy(true);
        
    }


    private void LayoutStaticObjects()
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {

                Vector3 position = new Vector3(x, ObjectsHeight, z);
                GameObject instance;
                if ((x == 0 || x == xSize - 1 || z == 0 || z == zSize - 1) || (x % 2 == 0 && z % 2 == 0))
                {
                    instance = Instantiate(StaticWallsPrefab, position, Quaternion.identity);
                    Field[x, z] = 1;
                }
                else
                {
                    position.y = floorHeight;
                    instance = Instantiate(FloorPrefab, position, Quaternion.identity);
                    Field[x, z] = 0;
                }
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    private void InstatiateFreeCells()
    {
        FreeCells = new List<Vector3>();
        for (int x = 1; x < xSize - 1; x++)
        {
            for (int z = 1; z < zSize - 1; z++)
            {
                if (!(x % 2 == 0 && z % 2 == 0))
                {
                    Vector3 freeCell = new Vector3(x, ObjectsHeight, z);
                    FreeCells.Add(freeCell);
                }
            }
        }
    }

    Vector3 GetRandomPosition(ref List<Vector3> list)
    {
        int index = Random.Range(0, list.Count);
        Vector3 randomPosition = list[index];
        list.RemoveAt(index);
        return randomPosition;
    }

    Vector3 LayoutObject(GameObject gObject)
    {
        Vector3 position = GetRandomPosition(ref FreeCells);
        GameObject instance = Instantiate(gObject, position, Quaternion.identity);
        instance.transform.SetParent(boardHolder);
        Field[(int)position.x, (int)position.z] = 1;
        return position;
    }

    void LayoutObjectsAtRandom(GameObject gObject, int count)
    {
        for (int i = 0; i < count; i++)
        {
           LayoutObject(gObject);
        }
    }

    void LayoutCollapsingWalls(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 position = LayoutObject(CollapsingWallsPrefab);
            FreeCollapsingWallsList.Add(position);
        }
    }


    void LayoutPlayer()
    {
        Vector3 position = GetRandomPosition(ref FreeCells);
        position.y -= 0.4f;
        Instantiate(PlayerPrefab, position, Quaternion.identity);
    }

    void LayoutEnemy(bool isEnemySmart)
    {
        Vector3 position = GetRandomPosition(ref FreeCells);
        position.y -= 0.4f;
        if (!isEnemySmart)
            Instantiate(EnemyPrefab, position, Quaternion.identity);
        else
            Instantiate(SmartEnemyPrefab, position, Quaternion.identity);
    }

    void LayoutPowerUp(GameObject powerup, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 position = GetRandomPosition(ref FreeCollapsingWallsList);
            var powerUp = Instantiate(powerup, position, Quaternion.identity);
            var powerUpScript = powerUp.GetComponent<PowerUp>();
            PowerUpsList.Add(powerUpScript);
            FreeCollapsingWallsList.Remove(position);
        }
    }


    public void DestroyCollapsingWall(Vector3 wallPos)
    {
        Field[(int)wallPos.x, (int)wallPos.z] = 0;
        foreach (var powerUp in PowerUpsList)
        {
            if (powerUp != null)
            {
                if (powerUp.transform.position == wallPos)
                {
                    powerUp.gameObject.SetActive(true);
                }
            }
        }
    }




    public void ChangeCollapsingWallTransparency()
    {
        int children = boardHolder.childCount;
        for (int i = 0; i < children; ++i)
        {
            GameObject collapsingWall = boardHolder.GetChild(i).gameObject;
            if (collapsingWall.tag == "CollapsingWall")
            {
                collapsingWall.GetComponent<MeshRenderer>().material.color = new Color(0.2f, 0.1f, .5f, 0.5f);
            }
        }

    }





}