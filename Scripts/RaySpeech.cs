using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaySpeech : MonoBehaviour {

	//public GameObject target;
	public InputField kuang;
	public GameObject flash;
	public Transform magicWood;
	private bool canUpdateState;

	private XmlDocManager xm;
	public GameObject ray;

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
		flash.SetActive(false);
	}
		
	public void StartSpeech(){
		//if(Input.GetKeyDown(KeyCode.M)){
			//speechLi ("Flash");
		//}
		//kuang.text = curses;
		AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
		jo.Call("StartActivity1");
	}
	public void speechLi(string str){
		kuang.text = str;
		List<string> allCurses = xm.GetXmlList ();
		string curses = "";
		for(int i = 0; i< allCurses.Count;i++){
			//curses += allCurses[i] + ":";
			if(allCurses[i].Equals(str)){
				setCurseOfFlash();
				break;
			}
		}
//		if(str=="Flash" || str=="flash"){
//			setCurseOfFlash();
//		}
	}

	void setCurseOfFlash(){
		Quaternion ori = GvrController.Orientation;
		GameObject ps = Instantiate (flash);
		ps.transform.position = new Vector3 (magicWood.transform.position.x-2.0f,
			magicWood.transform.position.y-0.2f, magicWood.transform.position.z + 1.0f);
		//ps.transform.rotation = ori;
		ps.transform.parent = this.gameObject.transform;
		ps.SetActive (true);
		//flash.transform.rotation = ray.transform.localRotation;
		//flash.SetActive (true);
	}
		
}
