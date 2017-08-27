using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifePoint : MonoBehaviour {

	private Text textField;
	private float fps = 60;
	public Camera cam;
	private PlayerState ps;

	void Awake(){
		textField = GetComponent<Text>();
		ps = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerState> ();
	}
	// Use this for initialization
	void Start () {
		if (cam == null) {
			cam = Camera.main;
		}
		if (cam != null) {
			// Tie this to the camera, and do not keep the local orientation.
			transform.SetParent(cam.GetComponent<Transform>(), true);
		}

	}
	
	// Update is called once per frame
	void Update () {
		int life = ps.GetCurrentLife ();
		if(life<0){
			life = 0;
		}
		textField.text = "HP:" + (float)life;
	}
}
