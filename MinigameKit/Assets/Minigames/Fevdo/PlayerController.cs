using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fevdo{
	public class PlayerController : PlayerInfo {
		[SerializeField]
		float speed;
		[SerializeField]
		float bowRotationSpeed;
		[SerializeField]
		float drawDistance;
		[SerializeField]
		float drawSpeed;
		[SerializeField]
		float drawCooldown;
		[SerializeField]
		Transform arrowIk;
		[SerializeField]
		Transform bowBone;
		[SerializeField]
		Transform arrowMaxDraw;
		[SerializeField]
		GameObject arrowPrefab;
		[SerializeField]
		Transform flipTransform;
		[SerializeField]
		GameObject arrowSlot;
		Vector3 arrowOriginalPosition;
		bool canDraw = true;
		float maxDrawDistance;

		// Use this for initialization
		public override void Start () {
			base.Start();
			arrowOriginalPosition = arrowIk.localPosition;
			maxDrawDistance = Vector3.Distance(arrowOriginalPosition, arrowMaxDraw.localPosition);
		}
		
		// Update is called once per frame
		void Update () {
			var hor = Input.GetAxisRaw(playerButtons.horizontal);
			var ver = Input.GetAxisRaw(playerButtons.vertical);
			Walk(hor);
			Aim(ver);
			if(Input.GetButton(playerButtons.action) && canDraw){
				Charge();
			}
			if(Input.GetButtonUp(playerButtons.action)){
				Release();
			}
		}

		void Walk(float hor){
			if(transform.eulerAngles.y * hor == 180 || transform.eulerAngles.y + hor == -1)
				transform.RotateAround(flipTransform.position, Vector3.up, 180);
				//transform.localScale = new Vector3(hor,1,1);			
			transform.position += Vector3.right * speed * hor * Time.deltaTime;
		}

		void Aim(float ver){
			Quaternion cur = transform.rotation;
			bowBone.Rotate(Vector3.forward * bowRotationSpeed * Time.deltaTime * ver);
		}

		void Charge(){
			//arrowIk.position += -arrowIk.right * drawSpeed * Time.deltaTime;
			arrowIk.localPosition = Vector3.Lerp(arrowIk.localPosition, arrowMaxDraw.localPosition , drawSpeed * Time.deltaTime);
		}
		void Release(){
			canDraw = false;
			var power = Vector3.Distance(transform.localPosition, arrowMaxDraw.localPosition) / Vector3.Distance(arrowMaxDraw.localPosition,  arrowOriginalPosition);
			var arrow = GameObject.Instantiate(arrowPrefab,arrowIk.position,bowBone.rotation);
			arrow.GetComponent<Arrow>().Launch(power);
			StartCoroutine(ReturnBow());

		}

		IEnumerator ReturnBow(){
			float counter = 0.0f;
			var distance = Vector3.Distance(arrowIk.localPosition, arrowOriginalPosition);
			while(counter < 1.0f){
				arrowIk.localPosition = Vector3.Lerp(arrowIk.localPosition, arrowOriginalPosition, counter);
				counter += Time.deltaTime/distance;
				yield return null;
			}
			counter = 1.0f;	
			canDraw = true;
		}
	}
}
