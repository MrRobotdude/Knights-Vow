using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class floodFill : MonoBehaviour
{
    List<Coord> AllTileCoords; Queue<Coord> shuffledTileCoords;
    public GameObject portal;

    int mapWidth = 10;
    int mapHeight = 15;
    int mapRatio = 5;
    int trapCount;
    Coord spawnPoint;
    public struct Coord
    {
        public int x;
        public int y;


        public Coord(int x2, int y2)
        {
            x = x2;
            y = y2;
        }
    }

    public void Start()
    {
        trapCount = mapWidth * mapHeight / mapRatio;
        spawnPoint = new Coord(mapWidth / 2, 0);
        lastTile();
        NewFloodFill();
        generateMap();
    }

    bool[,] obstacleMap;
    int currentTrapCount;
    public void NewFloodFill()

    {
        AllTileCoords = new List<Coord>();
        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 1; j < mapHeight - 2; j++)
            {
                AllTileCoords.Add(new Coord(i, j));
            }
        }
        shuffledTileCoords = new Queue<Coord>(Utility.ShuffleArray(AllTileCoords.ToArray(), Random.Range(100, 200)));


        for (int i = 0; i < trapCount; i++)
        {
            Coord randomCoord = getRandomCoord();
            obstacleMap[randomCoord.x, randomCoord.y] = true;
            currentTrapCount++;

            if (randomCoord.y != 0 && randomCoord.y != mapHeight - 1 && MapIsFullyAccessible(obstacleMap, currentTrapCount))
            {

            }
            else
            {
                obstacleMap[randomCoord.x, randomCoord.y] = false;
                currentTrapCount--;
            }
        }
    }

    public Coord getRandomCoord()
    {
        Coord randomCoord = shuffledTileCoords.Dequeue();
        shuffledTileCoords.Enqueue(randomCoord);
        return randomCoord;
    }

    bool MapIsFullyAccessible(bool[,] obstacleMap, int currentTrapCount)
    {
        bool[,] mapFlag = new bool[obstacleMap.GetLength(0), obstacleMap.GetLength(1)];
        Queue<Coord> queue = new Queue<Coord>();
        queue.Enqueue(spawnPoint);
        mapFlag[spawnPoint.x, spawnPoint.y] = true;

        int totalTile = 1;

        while (queue.Count > 0)
        {
            Coord tile = queue.Dequeue();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int neighbourX = tile.x + i;
                    int neighbourY = tile.y + j;

                    if (i == 0 || j == 0)
                    {
                        if (neighbourX >= 0 && neighbourY >= 0 && neighbourX < obstacleMap.GetLength(0) && neighbourY < obstacleMap.GetLength(1))
                        {
                            if (!mapFlag[neighbourX, neighbourY] && !obstacleMap[neighbourX, neighbourY])
                            {
                                mapFlag[neighbourX, neighbourY] = true;
                                queue.Enqueue(new Coord(neighbourX, neighbourY));
                                totalTile++;
                            }
                        }
                    }
                }

            }

        }

        int targetTile = mapWidth * (mapHeight) - currentTrapCount;
        return targetTile == totalTile;
    }

    public void lastTile()
    {
        obstacleMap = new bool[mapWidth, mapHeight];
        currentTrapCount = 0;
        for (int x = 0; x < mapWidth; x++)
        {
            obstacleMap[x, mapHeight - 2] = true;
            currentTrapCount++;

        }
        int random = Random.Range(0, mapWidth);
        
        obstacleMap[random, mapHeight - 2] = false;
        currentTrapCount--;
    }

    public GameObject floor;
    float offsetX = 3.47f;
    float offsetZ = 3.51f;

    public GameObject behindWallPrefabs;
    public GameObject leftWallPrefabs;
    public GameObject rightWallPrefabs;
    public GameObject frontWallPrefabs;
    public GameObject ceilingPrefabs;
    public GameObject door;
    public GameObject frontTorchPrefabs;
    public GameObject floortrap;
    public GameObject paladin;

    public void generateMap()
    {
        for(int i = 0; i<mapWidth; i++)
        {
            for(int j = 0; j < mapHeight; j++)
            {
                if (obstacleMap[i, j])
                {
                    Instantiate(floortrap, new Vector3(i * offsetX, 0, j * offsetZ), Quaternion.identity);
                }
                else
                {
                    Instantiate(floor, new Vector3(i * offsetX, 0, j * offsetZ), Quaternion.identity);
                }
                if(j == 0)
                {
                    GameObject behindWall = Instantiate(behindWallPrefabs);
                    behindWall.transform.position = new Vector3(i * offsetX , 0, j * offsetZ - 1.5f);
                    
                }
                if(i == 0)
                {
                    GameObject leftWall = Instantiate(leftWallPrefabs);
                    leftWall.transform.position = new Vector3(i * offsetX - 1.5f, 0, j * offsetZ);
                    
                }
                if(i == mapWidth-1)
                {
                    GameObject rightWall = Instantiate(rightWallPrefabs);
                    rightWall.transform.position = new Vector3(i * offsetX  + 1.5f, 0, j * offsetZ);
                    
                }
                if(j == mapHeight-1)
                {
                    GameObject wallFront = Instantiate(frontWallPrefabs);
                    wallFront.transform.position = new Vector3(i * offsetX, 0, j * offsetZ + 1.5f);
                }
                GameObject ceiling = Instantiate(ceilingPrefabs);
                ceiling.transform.position = new Vector3(i * offsetX, 5.5f, j * offsetZ);
                if (j%6 == 0 && i == mapWidth/2)
                {
                    GameObject torch = Instantiate(frontTorchPrefabs);
                    torch.transform.position = new Vector3(i * offsetX, 2.5f, j * offsetZ + 1.85f);
                }
            }
        }
        Instantiate(paladin, new Vector3(spawnPoint.x * offsetX, 0, spawnPoint.y * offsetZ), Quaternion.identity);
        CharacterStats.currentHealth = StaticStat.MaxHealth;
        GameObject doorPrefab = Instantiate(door);
        doorPrefab.transform.position = new Vector3(spawnPoint.x * offsetX, 0, spawnPoint.y * offsetZ - 1.25f);
        portal.transform.position = new Vector3(spawnPoint.x * offsetX, 2f, (mapHeight - 1) * offsetZ);
    }
    
}
