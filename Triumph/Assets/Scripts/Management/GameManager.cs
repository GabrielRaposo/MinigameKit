using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [Header("Organization")]
    public Arena arena;
    public UnitsManager unitsManager;
    public TurnManager turnManager;


	void Start () {
        StartCoroutine(Init());
	}

    IEnumerator Init()
    {
        arena.Init();
        yield return new WaitUntil(() => arena.isReady);
        unitsManager.SetUnits(Team.A, arena);
        unitsManager.SetUnits(Team.B, arena);

        turnManager.Init();
    }
}
