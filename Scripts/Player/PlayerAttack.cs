using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using PathologicalGames;
using System.Text;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum CurseStr  
{  
	Flame = 0,  
	Flash = 1,  
	Flare = 2,
	NONE  = 3,
	VALID = 4
} 

public class PlayerAttack : MonoBehaviour {

	//public CurseStr curseStr = CurseStr.Blaze;
	//1.Monster
	[SerializeField] private Fox fox;
	[SerializeField] private Snake snake;
	//[SerializeField] private Spider spider;
	
	//2.Curse
	public GameObject incendio_ps;
	public GameObject lightTrans_obj;
	private LightTransform lightTrans;
	public GameObject flash_ps;
	
	//public Transform ps_incendio;
	public GameObject prt;
	//Mana Bar
	public GameObject mana_bar_obj;
	private circleProcess mana_bar;
	private EnergyBar eb;
	public GameObject pos;
	//Ray
	private GvrPointerPhysicsRaycaster gvrRay;
	//Contrl is Down
	[SerializeField] private bool isAct = false;
	//音声入力文字列
	private string speechStr = "";
	[SerializeField] private Text speechPanel;
	//認識できない場合表示するための文字列
	StringBuilder textSpeech = new StringBuilder ();
	[SerializeField] private GameObject can;
	bool isCanSpeech = true;  //head
	bool isCanDo =false;
	//public InputField isf;
	public AudioSource death;

	void Awake () {
		//Rey取得
		gvrRay = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<GvrPointerPhysicsRaycaster> ();
		lightTrans = lightTrans_obj.GetComponent<LightTransform> ();
		mana_bar = mana_bar_obj.GetComponent<circleProcess> ();

	}

	void ChangeScene(){
		//Application.LoadLevel("MenuUI");
		SceneManager.LoadScene ("MenuUI");
	}

	void MonsterDown(){
		if(snake != null && snake.GetCurrentLife() <= 0){
			can.SetActive(true);
			textSpeech.Length = 0;
			speechPanel.text = textSpeech.Append ("クリアしました\nおめでとうございます！").ToString();
			Invoke ("ChangeScene", 4.0f);
		}
		if(fox != null && fox.GetCurrentLife() <= 0){
			can.SetActive(true);
			textSpeech.Length = 0;
			speechPanel.text = textSpeech.Append ("クリアしました!\nおめでとうございます！").ToString();
			Invoke ("ChangeScene", 4.0f);
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(GamePlayManager._instance.GetIsOver()){
			if(death != null && !death.isPlaying){
				death.Play ();
			}
			can.SetActive(true);
			textSpeech.Length = 0;
			speechPanel.text = textSpeech.Append (" 死亡！\n(ヒント：頭の部分を狙う)...").ToString();
			//StartCoroutine(SetManaDisper (can));
			return;
		}
		MonsterDown ();

		#if UNITY_EDITOR
		if(Input.GetKeyDown (KeyCode.X)){
			isAct = true;
		}else{
			isAct = false;
		}
		#endif
		#if UNITY_ANDROID && !UNITY_EDITOR
		if (GvrController.ClickButtonDown == true) {
			isAct = true;
		}else{
			isAct = false;
		}
		#endif
		#if UNITY_EDITOR
		if (isAct) {
			isCanDo = true;
		}
		#endif
		#if UNITY_ANDROID && !UNITY_EDITOR
		if (isAct) {
			speechStr = "";
			isCanDo = true;
			//StartCoroutine (BeginSpeech ());
			SpeechManager._instance.StartSpeech ();
		}
		speechStr = SpeechManager._instance.GetCurse ();
		#endif
		//jineng tiao
		//Mana Bar Show or Not
		if (mana_bar.GetCurrentAmout () >= 100) {
			mana_bar_obj.SetActive (false);
		}
		//2017/06/20 PC test
		//speechStr = "Flash";
		if (isCanDo == false) {
			return;
		}
		if ("".Equals (speechStr)) {
			speechStr = SpeechManager._instance.GetCurse ();
		} else {
			
			if (!"NULL".Equals (speechStr) && !"Begin".Equals (speechStr)
				&& !"End".Equals (speechStr) && !"Error".Equals (speechStr) && !"Start".Equals (speechStr)) {
				CurseStr curentStr = DoMana (speechStr);
				if (curentStr == CurseStr.NONE) {
					//UI表示2s後消し
					can.SetActive (true);
					textSpeech.Length = 0;
					textSpeech = textSpeech.Append ("登録されない呪文: ")
						.Append (speechStr);
					speechPanel.text = textSpeech.ToString ();
					isCanDo = false;
					StartCoroutine (SetManaDisper (can));
				} else if (curentStr == CurseStr.VALID) {
					//MP不足
					//UI表示2s後消し
					can.SetActive (true);
					textSpeech.Length = 0;
					speechPanel.text = textSpeech.Append ("MP不足\n補充中です！").ToString ();
					isCanDo = false;
					//StartCoroutine (SetManaDisper (can));
				} else {
					if ((snake != null && snake.GetIsGazeIn () == true)
					   || (fox != null && fox.GetIsGazeIn () == true)) {
						Attack (curentStr);
					}
					isCanDo = false;
					//StartCoroutine (SetManaDisper (can));
				}
				//isCanDo = false;
			} 
			else if (!"Error".Equals (speechStr)) {
				//can not get word
				can.SetActive (true);
				textSpeech.Length = 0;
				textSpeech = textSpeech.Append ("声を入力してください!")
					.Append (speechStr);
				speechPanel.text = textSpeech.ToString ();
				StartCoroutine (SetManaDisper (can));
				//isCanDo = false;
			} 
//			else if("Begin".Equals (speechStr)){
//				//can not get word
//				can.SetActive (true);
//				textSpeech.Length = 0;
//				textSpeech = textSpeech.Append ("声を入力してください");
//				speechPanel.text = textSpeech.ToString ();
//				//StartCoroutine (SetManaDisper (can));
//				//isCanDo = false;
//			}
//			else if ("Error".Equals (speechStr)) {
//				//can not get word
//				can.SetActive (true);
//				textSpeech.Length = 0;
//				textSpeech = textSpeech.Append ("タイムアウト");
//				speechPanel.text = textSpeech.ToString ();
//				StartCoroutine (SetManaDisper (can));
//				isCanDo = false;
//			}	
			else if ("Start".Equals (speechStr)) {
				//can not get word
				can.SetActive (true);
				textSpeech.Length = 0;
				textSpeech = textSpeech.Append ("認識準備中");
				speechPanel.text = textSpeech.ToString ();
				//StartCoroutine (SetManaDisper (can));
				//isCanDo = false;
			}
		}
	}

	IEnumerator BeginSpeech(){
		SpeechManager._instance.StartSpeech ();
		//yield return new WaitForSeconds(2.0f);
		yield return null;
	}
		    
	void Attack(CurseStr curentStr){
		
		if(snake!=null && snake.GetIsGazeIn()== true){
			snake.GetDamage(GetRightDamageByCurse(curentStr));
		}else if(fox!=null && fox.GetIsGazeIn() == true){
			if(fox.state == Fox.State.WALK){
				fox.GetDamage(GetRightDamageByCurse(curentStr));
			}
		}else if("Spider".Equals("")){
			//spider.GetDamage(GetRightDamageByCurse(curentStr));
		}
	}
		    
	private int GetRightDamageByCurse(CurseStr curentStr){
		int rtnDamage = 0;
		switch(curentStr){
		case CurseStr.Flame:
			rtnDamage = 30;
			break;
		case CurseStr.Flash:
			rtnDamage = 40;
			break;	
		default:
			rtnDamage = 0;
			break;					
		}
		return rtnDamage;
	}
		    
	private CurseStr DoMana(string speechWord){
		// get speech from speechManager
		//string speechWord = "BLINK";
		CurseStr curseStr = CurseStr.Flame;
		int currentMana =  mana_bar.GetCurrentAmout();
		if("Flame".Equals(speechWord)){
			//current mana
			if(currentMana <= 40){
				curseStr = CurseStr.VALID;
				return curseStr;
			}
			curseStr = CurseStr.Flame;
			mana_bar_obj.SetActive(true);
			mana_bar.ChangeManaCir(40);
			incendio_ps.SetActive(true);
			StartCoroutine (SetManaDisper(incendio_ps));
		}else if("Flare".Equals(speechWord)){
			if(currentMana <= 80){
				curseStr = CurseStr.VALID;
				return curseStr;
			}
			curseStr = CurseStr.Flare;
			//lightTrans_obj.gameObject.SetActive (true);
			mana_bar_obj.SetActive(true);
			mana_bar.ChangeManaCir(80);
			lightTrans.StartBlink ();
			//StartCoroutine (SetManaDisper(lightTrans_obj));
		}else if("Flash".Equals(speechWord)){
			//current mana
			if(currentMana <= 60){
				curseStr = CurseStr.VALID;
				return curseStr;
			}
			curseStr = CurseStr.Flash;
			mana_bar_obj.gameObject.SetActive(true);
			mana_bar.ChangeManaCir(60);
			flash_ps.SetActive(true);
			StartCoroutine (SetManaDisper(flash_ps,3));
		}else{
			curseStr = CurseStr.NONE;
		}
		return curseStr;
	}
	
	private IEnumerator SetManaDisper(GameObject obj_ps){
		yield return new WaitForSeconds(2.0f);
		obj_ps.SetActive(false);
	}
	private IEnumerator SetManaDisper(GameObject obj_ps, float time){
		yield return new WaitForSeconds(time);
		obj_ps.SetActive(false);
	}

	private void SetManaInvalid(){
		mana_bar.gameObject.SetActive(false);
		incendio_ps.gameObject.SetActive(false);
	}


}
