using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour {

    public float maxRange;
    public float speed;

    bool goRight;

    private void Start()
    {
        StartCoroutine(SpeedProgression());
    }

    void Update () {
        transform.position += Vector3.right * speed;
        if (transform.position.x > maxRange && speed > 0)
        {
            speed *= -1;
        }
        else
        if (transform.position.x < -maxRange && speed < 0)
        {
            speed *= -1;
        }
    }

    IEnumerator SpeedProgression()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            speed *= 1.1f;
        }
    }
}
