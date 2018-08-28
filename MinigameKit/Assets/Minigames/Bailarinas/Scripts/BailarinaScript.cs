using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//comentario fofo :3
namespace Bailarinas
{

    public class BailarinaScript : PlayerInfo
    {

        private bool lockedMovement;

        public float impulse;
        public float speed;
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
                rb.AddForce(new Vector3(300.0f, 0, 0));
            }

            if (Input.GetAxisRaw(playerButtons.vertical) >= 0.5f)
            {
                rb.AddForce(new Vector3(0, 0, 3.0f));
            }

            speed = rb.velocity.z;

        }

        private void FixedUpdate()
        {
            Move();            
            Rebalance();
        }

        void Move()
        {

            if(Input.GetAxisRaw(playerButtons.vertical) >= 1.0f && lockedMovement == false)
            {
                Step();
            }

            if(Input.GetAxisRaw(playerButtons.vertical) == 0 && lockedMovement == true)
            {
                lockedMovement = false;
            }

        }

        void Step()
        {
            rb.AddForce(Vector3.forward * impulse);
            Unbalance();
            lockedMovement = true;
        }

        void Unbalance()
        {
            float force = Random.Range(0.6f, 1.6f);
            int direction;

            if(transform.rotation.z >= 0)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }

            rb.AddTorque(Vector3.forward * direction * force * speed, ForceMode.Acceleration);
        }

        void Rebalance()
        {
            rb.AddTorque(Vector3.forward * Input.GetAxisRaw(playerButtons.horizontal) * 2.0f *-1, ForceMode.Acceleration);
            
        }

    }

}


