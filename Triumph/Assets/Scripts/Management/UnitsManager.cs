using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team {
    A,
    B
}

public class UnitsManager : MonoBehaviour {

    [Header("Values")]
    public Vector2Int[] spawnPosTeamA;
    public Vector2Int[] spawnPosTeamB;

    [Header("Units")]
    public ScriptableUnitData[] unitsTeamA;
    public ScriptableUnitData[] unitsTeamB;

    [Header("References")]
    public TurnManager turnManager;
    public GameObject prefabUnit;
    public Transform trasformTeamA;
    public Transform trasformTeamB;

    List<Unit> units;

    public int activeUnits { get; private set; }

    private void Awake()
    {
        units = new List<Unit>();
    }

    public void SetUnits (Team team, Arena arena) {
        Vector2Int[] spawnPositions;
        Transform teamTransform;
        ScriptableUnitData[] unitsData;

        switch (team)
        {
            default:
            case Team.A:
                spawnPositions = spawnPosTeamA;
                teamTransform = trasformTeamA;
                unitsData = unitsTeamA;
            break;

            case Team.B:
                spawnPositions = spawnPosTeamB;
                teamTransform = trasformTeamB;
                unitsData = unitsTeamB;
                break;
        }

        for (int i = 0; i < unitsTeamA.Length && i < spawnPositions.Length; i++)
        {
            GameObject unitObject = Instantiate(prefabUnit, Vector2.up * 100, Quaternion.identity, teamTransform);
            unitObject.SetActive(true);
            Unit unit = unitObject.GetComponent<Unit>();
            ScriptableUnitData unitData = unitsData[i];
            unit.SetUnit(this, team, spawnPositions[i], unitData.movement, unitData.health, unitData.damage, unitData.range);
            units.Add(unit);

            arena.PlaceUnit(unitObject, spawnPositions[i]);
        }
    }

    public Unit GetUnitByPosition(Vector2Int position)
    {
        foreach(Unit u in units)
        {
            if (u.position == position) return u;
        }
        return null; 
    }

    public void RefreshUnits(Team currentPhase)
    {
        activeUnits = 0;
        foreach (Unit u in units)
        {
            u.RefreshStamina();
            if (u.team == currentPhase) activeUnits++;
        }
    }

    public void NotifyDeactivation()
    {
        activeUnits--;
        if(activeUnits < 1)
        {
            turnManager.SwitchPhase();
        }
    }

    public void NotifyMovement(Vector2Int position, Team team)
    {
        int limitY = (team == Team.A) ? Arena.Y - 1 : 0;
        if (position.y == limitY)
        {
            Debug.Log("Ganhou!");
        }
    }
}
