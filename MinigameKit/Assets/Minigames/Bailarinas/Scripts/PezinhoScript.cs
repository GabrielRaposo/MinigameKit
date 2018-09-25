using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bailarinas
{
    public class PezinhoScript : MonoBehaviour
    {
        public float xAxis;        

        void Start()
        {
            xAxis = transform.position.x;
        }
    }

}

