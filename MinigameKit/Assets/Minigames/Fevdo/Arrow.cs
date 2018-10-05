using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fevdo{
	public class Arrow : MonoBehaviour {

		[SerializeField]
		float speed;

		Rigidbody2D rb;

		delegate void FlyPattern();
		FlyPattern fly;
		[SerializeField]
		Transform fixDummy;

		void Awake(){
			rb = GetComponent<Rigidbody2D>();
			fly = Nop;
		}

		public void Launch(float power){
			rb.AddForce(transform.up * speed);
			fly = RotateVelocity;
		}

		void FixedUpdate(){
			fly();
		}

		void RotateVelocity(){
			Vector2 v = rb.velocity;
			var angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), 1);
		}
		void Nop(){}
		void OnCollisionEnter2D(Collision2D coll){
			fly = Nop;
			Fixable fixable =  coll.gameObject.GetComponentInParent<Fixable>();
			if(fixable != null){
				fixable.Fix(fixDummy);
				Destroy(gameObject);
			}
		}
	}
}
