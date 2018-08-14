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

        private void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.CompareTag("Comida"))
            {
                Destroy(col.gameObject);
                
            }
        }
    }
}
