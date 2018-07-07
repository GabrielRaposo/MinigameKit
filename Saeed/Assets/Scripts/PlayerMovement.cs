using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    const float MIN_X_SPEED = .25f;

    [Header("Movement Values")]
    public float speedIncreaseRatio;
    public float speedDecreaseRatio;
    public float maxSpeed;
    public float jumpForce;
    public float jumpMaxHold;

    [Header("Extern Info")]
    public LayerMask groundLayer;
    public string BouncyTag;

    [Header("Component Reference")]
    public ParticleSystem bouncePS;

    string horizontalAxis, verticalAxis, jumpButton;
    bool onGround;
    Rigidbody2D _rigidbody;
    ScoreSystem scoreSystem;
    Coroutine jumpRoutine;

	void Start () {
        _rigidbody = GetComponent<Rigidbody2D>();
        scoreSystem = ScoreSystem.instance;
        InitStrings();
	}
	
    void InitStrings()
    {
        horizontalAxis = "Horizontal";
        verticalAxis = "Vertical";
        jumpButton = "Jump";
    }

	void Update () {
        CheckGround();
        HorizontalMovement();
        if (onGround)
        {
            JumpMovement();
            if (scoreSystem) scoreSystem.ResetValue();
        }
	}

    void CheckGround()
    {
        onGround = Physics2D.OverlapCircle(transform.position + (Vector3.down * .5f), .2f, groundLayer);
    }

    void HorizontalMovement()
    {
        float horizontalInput = Input.GetAxisRaw(horizontalAxis);
        Vector2 _velocity = _rigidbody.velocity;

        if(Mathf.Abs(horizontalInput) > 0)
        {
            _velocity += Vector2.right * speedIncreaseRatio * horizontalInput;
            if (_velocity.x > maxSpeed)
                _velocity.x = maxSpeed;
            if (_velocity.x < -maxSpeed)
                _velocity.x = -maxSpeed;
        }
        else
        { 
            if (_velocity.x > 0) {
                _velocity += Vector2.left  * (onGround ? speedDecreaseRatio : (speedDecreaseRatio / 2));
            } else
            if (_velocity.x < 0) {
                _velocity += Vector2.right * (onGround ? speedDecreaseRatio : (speedDecreaseRatio / 2));
            }

            if (Mathf.Abs(_velocity.x) < MIN_X_SPEED) _velocity.x = 0;
        }

        _rigidbody.velocity = _velocity;
    }

    void JumpMovement()
    {
        if (Input.GetButtonDown(jumpButton))
        {
            Jump();
            StartCoroutine(JumpHoldProperty());
        }
    }

    void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        _rigidbody.AddForce(Vector2.up * jumpForce);
    }

    IEnumerator JumpHoldProperty() {
        for (int i = 0; i < jumpMaxHold && Input.GetButton(jumpButton); i++)
        {
            yield return new WaitForEndOfFrame();
            Jump();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == BouncyTag)
        {
            Bounce();
            BounceProperty bounceProperty = collision.gameObject.GetComponent<BounceProperty>();
            if (bounceProperty)
            {
                bouncePS.Play();
                bounceProperty.StartTremble();
            }
            if (scoreSystem) scoreSystem.AddValue();
        }
    }


    void Bounce()
    {
        Jump();
        StartCoroutine(JumpHoldProperty());
    }
}
