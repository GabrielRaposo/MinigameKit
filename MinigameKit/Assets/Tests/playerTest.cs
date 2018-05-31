using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerTest : PlayerInfo {

    Rigidbody2D rb;
    int speed = 20;

	public override void Start () {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        GetComponent<Image>().color = this.color;
    }
	
	void Update () {
        rb.velocity = new Vector2(Input.GetAxisRaw(this.playerButtons.horizontal),
            Input.GetAxisRaw(this.playerButtons.vertical)) * speed;

        if (Input.GetButtonDown(this.playerButtons.action)) speed *= 2;
    }
}
