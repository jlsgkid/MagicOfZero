using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class migong : MonoBehaviour {
	
	StringBuilder textSpeech = new StringBuilder ();
	public Text uiText;
	public GameObject prt;
	private bool isDown = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(GvrController.ClickButtonDown == true){
			SpeechManager._instance.StartSpeech ();
			isDown = true;
		}
		string rtnStr = SpeechManager._instance.GetCurse ();
		//string rtnStr = "";
		//rtnStr = "Ok";
		if ("Okay".Equals (rtnStr)) {
			textSpeech.Length = 0;
			uiText.text = textSpeech.Append ("認識されました！準備できましたか？Go!").ToString ();
			SpeechManager._instance.SetCurse ();
			Destroy (prt.gameObject, 2.5f);
		} else if( !"".Equals(rtnStr) && isDown == true){
			textSpeech.Length = 0;
			uiText.text = textSpeech.Append ("認識されない、もう一回やってください！").ToString ();
		}
	}
}
