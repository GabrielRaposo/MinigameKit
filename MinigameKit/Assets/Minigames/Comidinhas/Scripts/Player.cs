using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Comidinhas
{
    public class Player : PlayerInfo {

	    


	    public override void Start() {

            base.Start();

            GetComponent<SpriteRenderer>().color = base.color;
	    }
	
	    
	    void Update () {
		    
	    }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Comida")
            {
                Destroy(col.gameObject);
            }
        }
    }
}
