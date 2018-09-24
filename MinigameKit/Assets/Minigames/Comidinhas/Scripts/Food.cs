using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Comidinhas
{
    public class Food : MonoBehaviour {

        FoodGen foodGen;
        Rigidbody2D rb;

        public void Init(FoodGen foodGen)
        {
            this.foodGen = foodGen;
            rb = GetComponent<Rigidbody2D>();
        }

        public void Launch(Vector2 direction)
        {
            rb.velocity = direction;
            rb.AddTorque(Random.Range(-45f, 45f));
        }

        public void Disable()
        {
            if (foodGen) foodGen.ReturnFood(gameObject);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Arena"))
            {
                Disable();
            }
        }
    }
}
