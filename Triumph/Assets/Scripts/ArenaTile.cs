using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaTile : MonoBehaviour, ISelectable {

    [Header("Debug")]
    public Color defaultColor;
    public Color selectedColor;

    Arena arena;
    [HideInInspector] public Vector2Int gridPosition;
    SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(Arena arena, Vector2Int gridPosition)
    {
        this.arena = arena;
        this.gridPosition = gridPosition;
    }

    private void OnMouseDown()
    {
        if (arena)
        {
            arena.CheckTile(gridPosition);
        }
    }

    public void Select()
    {
        _renderer.color = selectedColor;
    }

    public void Deselect()
    {
        _renderer.color = defaultColor;
    } 
}
