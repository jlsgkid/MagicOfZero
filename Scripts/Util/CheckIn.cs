using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIn : MonoBehaviour {

	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.CompareTag("Player")){

		}
	}
}
