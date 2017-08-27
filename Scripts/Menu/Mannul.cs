using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class Mannul : MonoBehaviour {
	StringBuilder textSpeech = new StringBuilder ();
	public Text uiText;
	private string guyou = "コントローラーの使い方：\n\n";
	[SerializeField] Sprite[] m_animPics;
	private bool isCanPlay1 = false;
	private bool isCanPlay2 = false;
	private bool isCanPlay3 = false;
	public int m_CurrentTextureIndex = 0;
	public  Image changeImg ;
	public  float timer = 0;
	[SerializeField] private int m_FrameRate = 30; 
	private WaitForSeconds m_FrameRateWait;
	private  int i = 0;
	[SerializeField] private DesMenu dm;
	public GameObject prt;
	private bool isDown = false;


	// Use this for initialization
	void Start () {
		isCanPlay1 = true;
		isCanPlay2 = false;
		isCanPlay3 = false;
		uiText.text = textSpeech.Append (guyou).ToString();
		m_FrameRateWait = new WaitForSeconds (1f / m_FrameRate);
	}
	
	// Update is called once per frame
	void Update () {
		if (isCanPlay1) {
			Invoke ("show1", 2.0f);
		}
		if(isCanPlay2) 
			Invoke ("show2", 1.0f);
		if(isCanPlay3) 
			Invoke ("show3", 2.0f);
	}

	void show1(){
		textSpeech.Length = 0;
		uiText.text = textSpeech.Append (guyou)
			.Append ("1.TouchPadの上下(左右)をタッチして移動できます。").ToString ();
		
		timer += Time.deltaTime;
		if(timer > 1){
			PlayTexture ();
			timer = 0;
		}
		TrackTouch ();
	}

	void show2(){
		changeImg.sprite = m_animPics [8];
		textSpeech.Length = 0;
		uiText.text = textSpeech.Append (guyou)
			.Append ("2.TouchPadをクリックして速くマイクに音声を入力したら呪文が発動できます。\nテスト呪文：Flash").ToString ();
		if(GvrController.ClickButtonDown == true){
			SpeechManager._instance.StartSpeech ();
			isDown = true;
		}
		string rtnStr = SpeechManager._instance.GetCurse ();
		//string rtnStr = "Flash";
		if ("Flash".Equals (rtnStr)) {
				textSpeech.Length = 0;
				uiText.text = textSpeech.Append (guyou)
				.Append ("認識されました！").ToString ();
				isCanPlay2 = false;
				isCanPlay3 = true;
		} else if( !"".Equals(rtnStr)&& isDown == true){
				textSpeech.Length = 0;
				uiText.text = textSpeech.Append (guyou)
				.Append ("認識されない、もう一回やってください！").ToString ();
		}
	}

	void show3(){
		SpeechManager._instance.SetCurse ();
		changeImg.sprite = m_animPics [9];
		textSpeech.Length = 0;
		uiText.text = textSpeech.Append (guyou)
			.Append ("3.APPButtonを押したら、呪文リストが表示できます。今使える呪文を確認してください！\nもう一回押したら閉じます。").ToString ();
		if(dm.isAppDown()){
			changeImg.sprite = m_animPics [10];
			textSpeech.Length = 0;
			uiText.text = textSpeech.Append (guyou)
				.Append ("さあ、魔法世界の冒険を始めましょう！\n幸運を祈っています〜").ToString ();
			isCanPlay3 = false;
			if(prt != null)
			Destroy (prt.gameObject, 3.5f);
		}
	}
		

	void TrackTouch(){
		if (GvrController.IsTouching) {
			Vector2 curPos = GvrController.TouchPos;
			if (GvrController.TouchPos.y < 0.2) {
				i += 1;
			} else if(GvrController.TouchPos.y>0.8){
				i += 1;
			}
			if(i>=3){
				isCanPlay1 = false;
				isCanPlay2 = true;
			}
		}

	}

	void PlayTexture(){
		changeImg.sprite = m_animPics [m_CurrentTextureIndex];
		if(m_CurrentTextureIndex == 7){
			m_CurrentTextureIndex = 0;
		}
		m_CurrentTextureIndex = m_CurrentTextureIndex + 1;
	}

	private IEnumerator PlayTextures (){

		while(isCanPlay1){
			changeImg.sprite = m_animPics [m_CurrentTextureIndex];
			if(m_CurrentTextureIndex == 7){
				m_CurrentTextureIndex = 0;
			}
			m_CurrentTextureIndex = m_CurrentTextureIndex + 1;
			yield return m_FrameRateWait;

		}
	}

}
