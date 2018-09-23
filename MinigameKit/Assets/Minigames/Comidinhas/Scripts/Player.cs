using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Comidinhas
{
    public class Player : PlayerInfo {

        public TextMeshProUGUI scoreboardText;
        public GameController gameController;
        public int score;

        [Header("Color replacement")]
        public Color baseColor;

        private enum PlayerState { Idle, Inflated, Stunned }
        private PlayerState state;
        private bool allowMovement;

        Animator animator;
        float playerSpeed = 100f;
        Vector3 x;

	    public override void Start()
        {
            base.Start();

            animator = GetComponent<Animator>();

            SpriteColorReplacement scr = GetComponent<SpriteColorReplacement>();
            scr.replacedColors = new List<SpriteColorReplacement.ReplacedColor>();
            scr.replacedColors.Add(new SpriteColorReplacement.ReplacedColor(baseColor, base.color));
            scr.Apply();
        }
	
	    
	    void Update ()
        {
            if (!allowMovement) return;

            switch (state)
            {
                case PlayerState.Idle:
                    Move();
                    if (Input.GetButtonDown(playerButtons.action))
                    {
                        Action();
                    }
                    break;
            }
        }


        private void Move()
        {
            x = new Vector3(Input.GetAxisRaw(playerButtons.horizontal), 0, 0);
            animator.SetInteger("MoveSpeed", (int)x.x);
            transform.Translate(x * playerSpeed * Time.deltaTime);
        }

        private void Action()
        {
            animator.SetTrigger("Inflate");
            state = PlayerState.Inflated;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(state == PlayerState.Idle)
            {
                if (col.gameObject.tag == "Comida")
                {
                    Destroy(col.gameObject);
                    animator.SetTrigger("Eat");
                    score++;
                    scoreboardText.text = score.ToString();
                } else 
                if (col.gameObject.tag == "Player") 
                {
                    //"Player" escolhido para tag do empurrão só para não criar tag nova no projeto
                    StartCoroutine(LaunchToDirection(
                            (transform.position.x < col.gameObject.transform.position.x) ? Vector3.left : Vector3.right
                    ));
                }
            }
        }

        public void ToggleMovement(bool value)
        {
            allowMovement = value;
            if (!value)
            {
                animator.SetInteger("MoveSpeed", 0);
            }
        }

        public void ReturnToIdle()
        {
            state = PlayerState.Idle;
        }


        IEnumerator LaunchToDirection(Vector3 direction)
        {
            animator.SetTrigger("Stun");
            state = PlayerState.Stunned;

            float speed = 5f;
            while (speed > 0)
            {
                transform.Translate(direction * speed);
                speed -= .2f;
                yield return new WaitForEndOfFrame();
            }

            animator.SetTrigger("Reset");
            state = PlayerState.Idle;
        }
    }
}
