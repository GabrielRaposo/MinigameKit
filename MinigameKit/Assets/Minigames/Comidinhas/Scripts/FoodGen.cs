using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Comidinhas
{
    public class FoodGen : MonoBehaviour {

        public GameObject[] foodPool = new GameObject[4];
        public float generationTime;
        public float max_x;
        public float min_x;


	    void Update () {
            //if (GameController.startGame)
            //{
                if(generationTime <= 0)
                {
                    generationTime = Random.Range(0.2f, 1.2f);
                    SpawnFood();
                }
                generationTime -= Time.deltaTime;
            //}
	    }

        void SpawnFood()
        {
            Instantiate(foodPool[Random.Range(0, 4)], new Vector3(Random.Range(-min_x, max_x), transform.position.y, 0f), transform.rotation);
        }
    }
}

