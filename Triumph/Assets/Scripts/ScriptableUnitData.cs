using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "UnitData")]
public class ScriptableUnitData : ScriptableObject {
    public int movement;
    public int health;
    public int damage;
    public int range;
}
