using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, IMoveable {
    const int MAX_STAMINA = 1;

    public Team team { get; private set; }
    public Vector2Int position { get; private set; }
    public bool disabled { get; private set; }

    public int movement { get; private set; }
    public int health { get; private set; }
    public int damage { get; private set; }
    public int range { get; private set; }

    int stamina;
    Color originalColor;
    SpriteRenderer _renderer;
    UnitsManager unitsManager;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void SetUnit(
        UnitsManager unitsManager, Team team, Vector2Int position, int movement, int health, int damage, int range)
    {
        this.unitsManager = unitsManager;
        this.team = team;
        this.position = position;

        this.movement = movement;
        this.health = health;
        this.damage = damage;
        this.range = range;

        if (team == Team.A) originalColor = Color.blue;
        else                originalColor = Color.red;
        SetDisabled(false);

        RefreshStamina();
    }

    public void Move(Vector2Int indexedPosition)
    {
        //DoMove
        position = indexedPosition;
        transform.position = Arena.IndexToWorldPosition(indexedPosition);
        DecreaseStamina(1);
        unitsManager.NotifyMovement(indexedPosition, team);
    }

    public void RefreshStamina()
    {
        stamina = MAX_STAMINA;
        SetDisabled(false);
    }

    void DecreaseStamina(int value)
    {
        stamina -= value;
        if (stamina < 1)
        {
            stamina = 0;
            SetDisabled(true);
        }
    }

    void SetDisabled(bool value)
    {
        if (value) {
            _renderer.color = Color.gray;
            unitsManager.NotifyDeactivation();
        } else {
            _renderer.color = originalColor;
        }
        disabled = value;
    }
}
