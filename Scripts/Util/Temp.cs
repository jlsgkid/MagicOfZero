using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Temp : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter(Collider c){
		Debug.Log ("trigger");
		if(c.gameObject.CompareTag("Player")){
			SceneManager.LoadScene ("Scene02");
		}
	}
}
