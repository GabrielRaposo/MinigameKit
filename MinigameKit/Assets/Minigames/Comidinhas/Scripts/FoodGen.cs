using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Comidinhas
{
    public class FoodGen : MonoBehaviour {

        public GameObject[] foodPool = new GameObject[2];
        public float generationTime;

	    void Start () {
		
	    }

	    void Update () {
            //if (GameController.startGame)
            //{
                if(generationTime <= 0)
                {
                    generationTime = Random.Range(0.2f, 1.5f);
                    SpawnFood();
                }
                Debug.Log(generationTime);
                generationTime -= Time.deltaTime;
            //}
	    }

        void SpawnFood()
        {
            Instantiate(foodPool[Random.Range(0, 2)], new Vector3(Random.Range(-100f, 100f), transform.position.y, 0f), transform.rotation);
        }
    }
}

