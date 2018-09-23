using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GataclismaNaPista
{
    public enum Direction { right, up, left, down };
    public enum ScoreType { fail, wrongArrow, almost, good, great, perfect };

    public class Arrow : MonoBehaviour
    {

        public Direction direction { get; private set; }
        public int duration { get; private set; } // duration é um integer aleatório de 1 até 2
        public Animator animator { get; private set; }

        public void Awake()
        {
            this.animator = GetComponent<Animator>();
        }

        public void Initialize(Direction direction, int duration)
        {
            this.direction = direction;
            this.duration = duration;
            this.transform.rotation = Quaternion.Euler(0, 0, (int)direction * 90);
        }

        /*public void Randomize()
        {
            this.duration = Random.Range(1, 3);
            this.direction = (Direction)Random.Range(0, 4);
            this.transform.rotation = Quaternion.Euler(0, 0, (int)direction * 90);
        }*/

        /*private void FixedUpdate()
        {
            Fall();
        }*/
    }
}
