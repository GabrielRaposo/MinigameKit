using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GataclismaNaPista
{
    public class ArrowSequence : MonoBehaviour
    {
        public Queue<GameObject> ArrowQueue { get; private set; }
        public GameObject arrowPrefab;
        public float arrowGap;
        public static float arrowSize;

        private float fallSpeed;
        private static float deadZone;
        private static float unqueueZone;
        private static float spawnZone;

        /*Essas variáveis aqui são meio gambiarras, eu acho? Tô inseguro*/
        public Arrow peekArrowScript { get; private set; } // Arrow que deve receber o input
        private GameObject unqueuedDeadArrow; // Arrow após passar a área de input

        private void Awake()
        {
            ArrowQueue = new Queue<GameObject>();
            arrowSize = arrowPrefab.GetComponent<SpriteRenderer>().bounds.size.y;
            spawnZone = (Camera.main.orthographicSize + arrowSize / 2);
            unqueueZone = this.transform.position.y - (this.GetComponent<SpriteRenderer>().bounds.size.y / 2 + arrowSize / 2);
            deadZone = -spawnZone;
        }

        private void Start()
        {
            Debug.Log(Time.fixedDeltaTime);
            fallSpeed = GameObject.FindObjectOfType<GameManager>().BPM / 60f * (arrowGap + arrowSize) * Time.fixedDeltaTime;
            /* Só tô trocando as cores das setas pra debugar, isso não vai acontecer no jogo */
        }

        private void Update()
        {
            CheckDeadZone();
        }

        private void FixedUpdate()
        {
            FallAllArrows();
        }

        void FallAllArrows()
        {
            if (unqueuedDeadArrow != null) unqueuedDeadArrow.transform.position += (Vector3.down * fallSpeed);
            foreach (GameObject arrow in ArrowQueue)
            {
                arrow.transform.position += (Vector3.down * fallSpeed);
            }
        }

        public void SpawnArrow(Direction direction, int duration)
        {
            GameObject newArrow = Instantiate(arrowPrefab, new Vector3(this.transform.position.x, spawnZone), Quaternion.identity, this.transform);
            newArrow.GetComponent<Arrow>().Initialize(direction, duration);
            ArrowQueue.Enqueue(newArrow);
            peekArrowScript = ArrowQueue.Peek().GetComponent<Arrow>();
            ArrowQueue.Peek().GetComponent<SpriteRenderer>().color = Color.cyan;
        }

        public void DestroyPeek(ScoreType score)
        {
            /*distance é variável gambiarra aqui:*/
            float distance = Mathf.Abs(this.transform.position.y - peekArrowScript.transform.position.y);
            if (score != ScoreType.fail && /*Gambiarra:*/ distance < PlayerController.almostDistance)
            {
                if (score == ScoreType.wrongArrow)
                {
                    //gambiarra if
                    if (unqueuedDeadArrow != null) Destroy(unqueuedDeadArrow);
                    unqueuedDeadArrow = ArrowQueue.Peek();
                    unqueuedDeadArrow.GetComponent<SpriteRenderer>().color = Color.gray;
                }
                else
                {
                    peekArrowScript.animator.Play("ArrowExplode");
                    Destroy(ArrowQueue.Peek(), 1f);
                }
                ArrowQueue.Dequeue();
                peekArrowScript = ArrowQueue.Peek().GetComponent<Arrow>();
                ArrowQueue.Peek().GetComponent<SpriteRenderer>().color = Color.cyan;
            }
        }

        private void CheckDeadZone()
        {
            if (ArrowQueue.Count > 0 && ArrowQueue.Peek().transform.position.y < unqueueZone)
            {
                unqueuedDeadArrow = ArrowQueue.Peek();
                ArrowQueue.Dequeue();
                unqueuedDeadArrow.GetComponent<SpriteRenderer>().color = Color.gray;
                peekArrowScript = ArrowQueue.Peek().GetComponent<Arrow>();
                ArrowQueue.Peek().GetComponent<SpriteRenderer>().color = Color.cyan;
            }
            if (unqueuedDeadArrow != null && unqueuedDeadArrow.transform.position.y < deadZone)
            {
                Destroy(unqueuedDeadArrow);
            }
        }

        // Apenas para mostrar a linha de spawn e de destroy no Gizmos o3o
        private void OnDrawGizmosSelected()
        {
            float cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
            Gizmos.color = Color.green;
            Gizmos.DrawLine(new Vector3(-cameraWidth, spawnZone), new Vector3(cameraWidth, spawnZone));
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(-cameraWidth, deadZone), new Vector3(cameraWidth, deadZone));
        }
    }
}
