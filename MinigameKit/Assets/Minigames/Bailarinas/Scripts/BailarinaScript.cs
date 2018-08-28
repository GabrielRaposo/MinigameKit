using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//comentario fofo :3
namespace Bailarinas
{

    public class BailarinaScript : PlayerInfo
    {

        private bool lockedMovement;

        public Transform pivot;
        public float fixedAxis;
        public float impulse;
        public float speed;
        private Rigidbody rb;

        // Use this for initialization
        override public void Start()
        {
            base.Start();
            rb = GetComponent<Rigidbody>();
            Unbalance(2.0f, 1);

            fixedAxis = transform.position.x;
            
        }
                
        void Update()
        {
                        
            speed = rb.velocity.z;

        }

        private void FixedUpdate()
        {
            Move();            
            Rebalance();


            float angle = transform.rotation.eulerAngles.z;
            if(angle > 180)
            {
                angle -= 360;
            }

            if (Mathf.Abs(angle) > 50.0f)
            {
                Debug.Log("DEAD");
                rb.constraints = RigidbodyConstraints.None;
            }
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
            rb.AddForce(Vector3.forward * impulse, ForceMode.Acceleration);
            Unbalance();
            lockedMovement = true;
        }

        void Unbalance()
        {
            float force = Random.Range(0.85f, 2.5f);
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

        void Unbalance(float force, int direction)
        {

            rb.AddTorque(Vector3.forward * direction * force, ForceMode.Acceleration);

        }

        void Rebalance()
        {
            rb.AddTorque(Vector3.forward * Input.GetAxisRaw(playerButtons.horizontal) * 2.0f *-1, ForceMode.Acceleration);
            
        }

    }

}


