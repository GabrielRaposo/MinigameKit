using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PieceType {
    Empty,
    Unit,
    Structure
}

public class Arena : MonoBehaviour {
    #region Variables
    static public int X = 7, Y = 5;

    [Header("Prefabs")]
    public GameObject prefabArenaTile;

    [Header("Component References")]
    public UnitsManager unitsManager;
    public UnitInputWindow inputWindow;
    public InputRegionManager inputRegionManager;

    enum InputState { Disabled, Menu, Default, Move, Attack }
    InputState inputState;

    public bool isReady { get; private set; }

    PieceType[,] arenaMatrix;
    ArenaTile[,] tiles;
    ArenaTile selectedTile;
    Unit selectedUnit;
    List<Vector2Int> inputRegions;
    Team currentPhase;

    #endregion

    public void Init () {
        CreateTerrain();
        inputRegionManager.CreateInputTilesList();
        SetDisabledState();
    }

    #region Terrain
    private void CreateTerrain()
    {
        arenaMatrix = new PieceType[X, Y];
        tiles = new ArenaTile[X, Y];

        for(int i = 0; i < X; i++)
        {
            for(int j = 0; j < Y; j++)
            {
                Vector2Int position = new Vector2Int(i, j);
                GameObject tileObject = Instantiate(prefabArenaTile, IndexToWorldPosition(position), Quaternion.identity, transform);
                tileObject.SetActive(true);
                tiles[i, j] = tileObject.GetComponent<ArenaTile>();
                tiles[i, j].Initialize(this, position);

                arenaMatrix[i, j] = PieceType.Empty;
            }
        }

        isReady = true;
    }

    static public Vector2 IndexToWorldPosition(Vector2Int indexedPosition)
    {
        return new Vector2((indexedPosition.x - X / 2) * 2, (indexedPosition.y - Y / 2) * 2);
    }
    #endregion

    #region Units
    public void PlaceUnit(GameObject unit, Vector2Int position)
    {
        unit.transform.position = IndexToWorldPosition(position);
        arenaMatrix[position.x, position.y] = PieceType.Unit;
    }
    #endregion

    #region Input State
    public void CheckTile(Vector2Int position)
    {
        switch (inputState)
        {
            default:
                break;

            case InputState.Default:
                DefaultSelection(position);
            break;

            case InputState.Move:
                MoveSelection(position);
                break;
        }
    }

    void SetDisabledState()
    {
        ResetSelection();
        inputState = InputState.Disabled;
    }

    void SetMenuState()
    {
        inputState = InputState.Menu;
    }

    void SetDefaultState()
    {
        ResetSelection();
        inputState = InputState.Default;
    }

    void SetMoveState(Vector2Int position)
    {
        inputRegions = new List<Vector2Int>();
        inputRegions = inputRegionManager.SetMovementRegion(inputRegions, arenaMatrix, position, position, selectedUnit.movement);
        inputState = InputState.Move;
    }
    #endregion

    public void EnableInput(Team currentPhase)
    {
        this.currentPhase = currentPhase;
        unitsManager.RefreshUnits(currentPhase);
        ClearInputRegions();
        SetDefaultState();
    }

    public void DisableInput()
    {
        SetDisabledState();
    }

    void DefaultSelection(Vector2Int position)
    {
        switch (arenaMatrix[position.x, position.y])
        {
            case PieceType.Unit:
                SetSelection(position);
                if(!selectedUnit.disabled && selectedUnit.team == currentPhase)
                {
                    inputWindow.Show(IndexToWorldPosition(position));
                    SetMenuState();
                }
                //SetMoveState(position);
                break;

            default:
                SetDefaultState();
                break;
        }
    }

    void SetSelection(Vector2Int position)
    {
        ResetSelection();

        ArenaTile newSelection = tiles[position.x, position.y];
        newSelection.Select();
        selectedTile = newSelection;
        selectedUnit = unitsManager.GetUnitByPosition(position);
    }

    void ResetSelection()
    {
        if (selectedTile != null) selectedTile.Deselect();
        if (selectedUnit != null) selectedUnit = null;
    }

    void MoveSelection(Vector2Int position)
    {
        if (inputRegions.Contains(position)) {
            selectedUnit = unitsManager.GetUnitByPosition(selectedTile.gridPosition);
            MoveUnit(selectedUnit, position);
            ClearInputRegions();
            SetDefaultState();
        } else {
            ClearInputRegions();
            DefaultSelection(position); 
        }
    }

    void MoveUnit(Unit unit, Vector2Int position)
    {
        arenaMatrix[unit.position.x, unit.position.y] = PieceType.Empty;
        arenaMatrix[position.x, position.y] = PieceType.Unit;
        unit.Move(position);
    }

    void ClearInputRegions()
    {
        inputRegions = new List<Vector2Int>();
        inputRegionManager.Clear();
    }
}
