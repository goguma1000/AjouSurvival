using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScrolling : MonoBehaviour
{
    [SerializeField]Transform playerTransform;
    Vector2Int currentTilePosition = new Vector2Int(0, 0);
    GameObject[,] terrainTiles;
    [SerializeField] float tileSize = 10f;
    [SerializeField] int terrainTileHorizontalCount;
    [SerializeField] int terrainTileVerticalCount;
    [SerializeField] Vector2Int  playerTilePosition;
    Vector2Int onTileGridPlayerPosition;
    [SerializeField] int fieldOfVisionHeight = 4;
    [SerializeField] int fieldOfVisionWidth = 4;


    private void Awake()
    {
        terrainTiles = new GameObject[terrainTileHorizontalCount, terrainTileVerticalCount];
        
    }
    private void Start()
    {
        UpdateTilesOnScreen();
    }

    private void Update()
    {
        playerTilePosition.x = (int)(playerTransform.position.x / tileSize);
        playerTilePosition.y = (int)(playerTransform.position.y / tileSize);

        playerTilePosition.x -= playerTransform.position.x < 0 ? 1 : 0;
        playerTilePosition.y -= playerTransform.position.y < 0 ? 1 : 0;

        if (currentTilePosition != playerTilePosition)
        {
            currentTilePosition = playerTilePosition;

            onTileGridPlayerPosition.x = CaluculatePositionOnAxis(onTileGridPlayerPosition.x, true);
            onTileGridPlayerPosition.y = CaluculatePositionOnAxis(onTileGridPlayerPosition.y, false);
            UpdateTilesOnScreen();
        }
    }

    private void UpdateTilesOnScreen()
    {
        for(int pov_x = -(fieldOfVisionWidth/2); pov_x < fieldOfVisionWidth; pov_x++)
        {
            for (int pov_y = -(fieldOfVisionHeight/2); pov_y <fieldOfVisionHeight; pov_y++)
            {
                int tileToUpdate_x = CaluculatePositionOnAxis(playerTilePosition.x + pov_x, true);
                int tileToUpdate_y = CaluculatePositionOnAxis(playerTilePosition.y + pov_y, false);

                GameObject tile = terrainTiles[tileToUpdate_x, tileToUpdate_y];
                tile.transform.position = CalculatTilePosition(playerTilePosition.x + pov_x, playerTilePosition.y + pov_y);
            }
        }
    }

    private Vector3 CalculatTilePosition(int x, int y)
    {
        return new Vector3(x * tileSize, y*tileSize, 0f);
    }

    private int CaluculatePositionOnAxis(float currentValue, bool horizintal)
    {
        if(horizintal)
        {
            if (currentValue >= 0)
            {
                currentValue = currentValue % terrainTileHorizontalCount;
            }
            else
            {
                currentValue += 1;
                currentValue = terrainTileHorizontalCount - 1 + currentValue % terrainTileHorizontalCount;
            }
        }
        else
        {
            if (currentValue >= 0)
            {
                currentValue = currentValue % terrainTileVerticalCount;
            }
            else
            {
                currentValue += 1;
                currentValue = terrainTileVerticalCount - 1 + currentValue % terrainTileVerticalCount;
            }
        }

        return (int)currentValue;

    }

    public void Add(GameObject gameObject, Vector2Int TilePosition)
    {
        terrainTiles[TilePosition.x, TilePosition.y] = gameObject;
    }
}
