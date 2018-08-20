using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRegionManager : MonoBehaviour {

    [Header("Temp")]
    public Color colorMovement;
    public Color colorAttack;
    public Color colorHeal;

    [Header("Values")]
    public int listSize = 15;
    public GameObject prefabInputTile;

    List<GameObject> inputTiles;
    int index;

    public void CreateInputTilesList()
    {
        inputTiles = new List<GameObject>();
        for (int i = 0; i < listSize; i++)
        {
            GameObject inputTile = Instantiate(prefabInputTile, Vector2.up * 100, Quaternion.identity, transform);
            inputTile.SetActive(false);
            inputTiles.Add(inputTile);
        }
    }

    public List<Vector2Int> SetMovementRegion(
        List<Vector2Int> regionList, PieceType[,] arenaMatrix, Vector2Int center, Vector2Int currentMiddle, int movement)
    {
        Vector2Int[] checkDirections = { Vector2Int.up, Vector2Int.left, Vector2Int.down, Vector2Int.right };
        int maxX = Arena.X, maxY = Arena.Y;

        if (movement > 0)
        {
            for(int i = 0; i < checkDirections.Length; i++)
            {
                Vector2Int checkPosition = currentMiddle + checkDirections[i];
                if (checkPosition.x < 0 || checkPosition.x > maxX - 1 || checkPosition.y < 0 || checkPosition.y > maxY - 1)
                    continue;
                if (!regionList.Contains(checkPosition)
                    && arenaMatrix[checkPosition.x, checkPosition.y] == PieceType.Empty 
                    && checkPosition != center)
                {
                    ActivateTile(checkPosition);
                    regionList.Add(checkPosition);
                    regionList = SetMovementRegion(regionList, arenaMatrix, center, checkPosition, movement - 1);
                }

            }
        }
        return regionList;
    }

    GameObject ActivateTile(Vector2Int position)
    {
        GameObject inputTile = inputTiles[index];
        inputTile.transform.position = Arena.IndexToWorldPosition(position);
        inputTile.SetActive(true);

        index = (index + 1) % inputTiles.Count;
        return inputTile;
    }

    public void Clear()
    {
        foreach (GameObject tile in inputTiles)
        {
            tile.transform.position = Vector2.up * 100; 
            tile.SetActive(false);
        }
        index = 0;
    }
}
