using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bailarinas
{

    public class BailarinaScript : PlayerInfo
    {
  
        private Rigidbody rb;

        // Use this for initialization
        override public void Start()
        {
            base.Start();
            rb = GetComponent<Rigidbody>();
            
        }
                
        void Update()
        {
            if (Input.GetButtonDown(playerButtons.action))
            {
                rb.AddForce(new Vector3(0, 0, 300.0f));
            }

            if (Input.GetAxisRaw(playerButtons.vertical) >= 0.5f)
            {
                rb.AddForce(new Vector3(0, 0, 3.0f));
            }

        }
    }

}


