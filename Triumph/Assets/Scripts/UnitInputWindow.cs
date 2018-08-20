using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitInputWindow : MonoBehaviour {

    [Header("Components")]
    public Button buttonMove;
    public Button buttonAttack;
    public Button buttonEnd;

    [Header("References")]
    public Arena arena;

    void Start () {
        InitButtons();
    }

    void InitButtons()
    {
        buttonMove.onClick.AddListener(() => MoveInput());
        buttonMove.gameObject.SetActive(false);

        buttonAttack.onClick.AddListener(() => AttackInput());
        buttonAttack.gameObject.SetActive(false);

        buttonEnd.onClick.AddListener(() => EndInput());
        buttonEnd.gameObject.SetActive(false);
    }

    public void MoveInput()
    {
        Debug.Log("move");
    }
    public void AttackInput()
    {
        Debug.Log("Attack");
    }
    public void EndInput()
    {
    }

    public void Show(Vector2 position)
    {
        //Refazer direitinho
        //transform.position = position;

        buttonMove.gameObject.SetActive(true);
        buttonAttack.gameObject.SetActive(true);
        buttonEnd.gameObject.SetActive(true);
    }
}
