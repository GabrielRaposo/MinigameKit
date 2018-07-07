using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceProperty: MonoBehaviour {
    public float squishSpeed = 0.01f;
    public float quantTrembles;
    public float trembleForce;
    public float trembleDecrease;

    SpriteRenderer _renderer;
    Shader shaderDefault;
    Shader shaderGUItext;

    Coroutine trembleRoutine;
    Vector2 originalScale;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        shaderDefault = _renderer.material.shader;
        shaderGUItext = Shader.Find("GUI/Text Shader");
        originalScale = transform.localScale;
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.S))
            StartTremble();
	}

    public void StartTremble()
    {
        if (trembleRoutine != null) StopCoroutine(trembleRoutine);
        trembleRoutine = StartCoroutine(TrembleAnimation());
        StartCoroutine(WhiteFlash());
    }

    IEnumerator TrembleAnimation()
    {
        transform.localScale = originalScale 
            + (Vector2.down  * trembleForce * 2) 
            + (Vector2.right * trembleForce * 2);

        float tForce = trembleForce;

        for (int i = 0; i < quantTrembles; i++)
        {
            yield return HorizontalSquish(tForce);

            transform.localScale = originalScale;
            tForce -= trembleDecrease;
        }
        yield return new WaitForEndOfFrame();
    }

    IEnumerator HorizontalSquish (float force)
    {
        bool press = true;
        while (true)
        {
            if (press) {
                transform.localScale += Vector3.right * squishSpeed;
                transform.localScale += Vector3.down  * squishSpeed;
                if (transform.localScale.x > originalScale.x + force)
                {
                    press = false;
                }
            } else {
                transform.localScale += Vector3.left * squishSpeed;
                transform.localScale += Vector3.up   * squishSpeed;
                if (transform.localScale.x < originalScale.x)
                {
                    break;
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator WhiteFlash()
    {
        _renderer.material.shader = shaderGUItext;
        for (int i = 0; i < 3; i++) yield return null;
        _renderer.material.shader = shaderDefault;
    }
}
