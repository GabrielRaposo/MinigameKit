using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Comidinhas
{
    public class FoodGen : MonoBehaviour {

        public GameObject[] foodPrefabs;
        public int quantity;

        public float max_x;
        public ParticleSystem vanishEffect;

        float generationTime;
        GameObject[] pool;
        int index;
        bool active = true;

        private void Start()
        {
            pool = new GameObject[quantity];
            for (int i = 0; i < quantity; i++)
            {
                pool[i] = Instantiate(foodPrefabs[i % foodPrefabs.Length], transform);
                pool[i].SetActive(false);
                pool[i].GetComponent<Food>().Init(this);
            }
            index = 0;
        }

        private void Update ()
        {
            if (!active) return;

            if(generationTime <= 0)
            {
                generationTime = Random.Range(.2f, .8f);
                SpawnFood();
            }
            generationTime -= Time.deltaTime;
	    }

        private void SpawnFood()
        {
            GameObject food = pool[index];
            food.transform.position = new Vector3(Random.Range(0, max_x) * (index % 2 == 0 ? 1 : -1), transform.position.y, 0);
            food.SetActive(true);
            Vector3 direction = food.transform.position.x > 0 ? Vector3.left : Vector3.right;
            direction *= Random.Range(0f, 25f);
            food.GetComponent<Food>().Launch(direction);

            index = (index + 1) % pool.Length;
        }

        public void ReturnFood(GameObject food)
        {
            vanishEffect.transform.position = food.transform.position;
            vanishEffect.Play();
            food.SetActive(false);
        } 

        public void SetActivation(bool value)
        {
            if (!value)
            {
                StartCoroutine(DisableAll());
            }
            active = value;
        }

        IEnumerator DisableAll()
        {
            List<GameObject> activeFood = new List<GameObject>();
            foreach (GameObject food in pool) { if (food.activeSelf) activeFood.Add(food); }

            foreach (GameObject food in activeFood)
            {
                ReturnFood(food);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}

