using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public struct TileCoords
    {
        public int x, y;
        public bool visited;
        public GameObject tile;

        public TileCoords(int _x, int _y)
        {
            x = _x;
            y = _y;
            tile = default;
            visited = false;
        }

        public static bool operator ==(TileCoords first, TileCoords second)
        {
            return first.x == second.x || first.y == second.y;
        }

        public static bool operator !=(TileCoords first, TileCoords second)
        {
            return first.x != second.x || first.y != second.y;
        }
    }

    public GameObject[] tiles;
    public GameObject goalTile;
    public GameObject startTile;
    public int tileHeight, tileWidth;
    public int mapSizeX, mapSizeY;

    private TileCoords startPos;
    private TileCoords goalPos;
    private TileCoords[,] tileMap;
    private Transform playerTransform;

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        tileMap = new TileCoords[mapSizeX, mapSizeY];
        startPos = new TileCoords((int)(mapSizeX / 2), 0);
        goalPos = GetGoalPosition();

        for (int i = 0; i < mapSizeX; i++)
        {
            for (int j = 0; j < mapSizeY; j++)
            {
                if ((i != startPos.x || j != startPos.y) && (i != goalPos.x || j != goalPos.y))
                {
                    tileMap[i, j] = SpawnRandomTile(i, j);
                }
                else if (i == goalPos.x || j == goalPos.y)
                {
                    tileMap[i, j] = SpawnSpecificTile(i, j, goalTile, goalPos);
                }
                else
                {
                    tileMap[i, j] = SpawnSpecificTile(i, j, startTile, startPos);
                }
            }
        }
    }

    void Update()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                if (tileMap[x, y] != startPos && tileMap[x, y] != goalPos)
                {
                    if (tileMap[x, y].visited && !WithinRange(x, y))
                    {
                        Destroy(tileMap[x, y].tile);
                        tileMap[x, y] = SpawnRandomTile(x, y);
                    }
                    else if (!tileMap[x, y].visited && WithinRange(x, y))
                    {
                        tileMap[x, y].visited = true;
                    }
                }
            }
        }
    }

    public Vector2 GetRandomVectorPosition()
    {
        int randomX = 0;
        int randomY = 0;
        Vector2 positon = Vector2.zero;

        do
        {
            randomX = (int)Random.Range(0, mapSizeX);
            randomY = (int)Random.Range(0, mapSizeY);
            positon = new Vector2((-mapSizeX / 2 + 0.5f + randomX) * tileWidth, (-mapSizeY / 2 + 0.5f + randomY) * tileHeight);
        }
        while (tileMap[randomX, randomY] == startPos || tileMap[randomX, randomY] == goalPos);

        return positon;
    }

    public Vector2 GetStartPlayerPosition()
    {
        return new Vector2((-mapSizeX / 2 + 0.5f + startPos.x) * tileWidth, (-mapSizeY / 2 + 0.5f + startPos.y) * tileHeight);
    }

    public Vector2 GetGoalVectorPosition()
    {
        return new Vector2((-mapSizeX / 2 + 0.5f + goalPos.x) * tileWidth, (-mapSizeY / 2 + 0.5f + goalPos.y) * tileHeight);
    }

    private TileCoords GetGoalPosition()
    {
        return new TileCoords((int)Random.Range(0, mapSizeX), (int)Random.Range((int)mapSizeY / 2, mapSizeY));
    }

    private bool WithinRange(int x, int y)
    {
        float playerX = playerTransform.position.x;
        float playerY = playerTransform.position.y;
        float tileX = (-mapSizeX / 2 + 0.5f + x) * tileWidth;
        float tileY = (-mapSizeY / 2 + 0.5f + y) * tileHeight;

        return (playerX > tileX - tileWidth && playerX < tileX + tileWidth && playerY > tileY - tileHeight && playerY < tileY + tileHeight);
    }

    private TileCoords SpawnRandomTile(int indexX = -1, int indexY = -1)
    {
        TileCoords coord = new TileCoords(indexX, indexY);
        int tileToInstantiate = (int)Random.Range(0, tiles.Length);
        return SpawnTile(indexX, indexY, tiles[tileToInstantiate], coord);
    }

    private TileCoords SpawnSpecificTile(int indexX, int indexY, GameObject tileToInstantiate, TileCoords coord)
    {
        return SpawnTile(indexX, indexY, tileToInstantiate, coord);
    }

    private TileCoords SpawnTile(int indexX, int indexY, GameObject tileToInstantiate, TileCoords coord)
    {
        GameObject gameObject;

        gameObject = Instantiate(tileToInstantiate) as GameObject;
        gameObject.transform.SetParent(transform);
        gameObject.transform.position = new Vector2((-mapSizeX/2 + 0.5f + indexX) * tileWidth, (-mapSizeY/2 + 0.5f + indexY) * tileHeight);
        coord.tile = gameObject;

        return coord;
    }
}