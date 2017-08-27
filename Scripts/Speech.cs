using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speech : MonoBehaviour {

	//public GameObject target;
	public InputField kuang;
	public Material speechMaterial;
	public Material orgMaterial;
	public bool isChange = true;

	public GameObject flash;
	public Transform magicWood;

	private bool canUpdateState;

	private XmlDocManager xm;
	// Use this for initialization
	void Start () {
		canUpdateState = true;
		xm = GameObject.FindGameObjectWithTag ("XML").GetComponent<XmlDocManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (canUpdateState) {
			canUpdateState = false;
			Invoke("DeactivateThis", 3.0f);
		}
	}

	void DeactivateThis()
	{
		//flash.SetActive(false);
	}
		
	public void StartSpeech(){
		List<string> allCurses = xm.GetXmlList ();
		string curses = "";
		for(int i = 0; i< allCurses.Count;i++){
			curses += allCurses[i] + ":";
		}
		kuang.text = curses;
		AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
		jo.Call("StartActivity1");
	}
	public void speechLi(string str){
		//Debug.Log (isChange);
		//str = "变色";
		//kuang.text = str;
		if(str=="Flash" || str=="flash"){
			//GetComponent<Renderer>().material = isChange ? speechMaterial:orgMaterial;
			//ChangeColor ();
			setCurseOfFlash();
		}

	}

	void setCurseOfFlash(){
		Quaternion ori = GvrController.Orientation;
		flash.transform.position = new Vector3 (flash.transform.position.x,
			magicWood.transform.position.y-0.2f, flash.transform.position.z);
		//flash.transform.rotation = ori;
		flash.SetActive (true);
	}

	void ChangeColor(){
		//flash.SetActive (true);
		string name = GetComponent<Renderer> ().material.name;
		if (name.Contains ("yellow")) {
			GetComponent<Renderer> ().material = speechMaterial;
		} else {
			GetComponent<Renderer> ().material = orgMaterial;
		}
	}
}
