using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class MenuChangeText : MonoBehaviour {

	public Text title;
	public Text att_text;
	//public Text des_text;
	public Text diff_text;
	public Text mp_text;
	StringBuilder textSpeech = new StringBuilder ();
	public RectTransform nimg;
	public RectTransform mpimg;

	void Start(){
		FireEnter ();
		diff_text.text = "難易度:";
		mp_text.text = "MP:";
	}

	public void FireEnter(){
		//Flame
		title.text = "<color=red><size=45>Flame</size></color>\n発音：フレイム";
		textSpeech.Length = 0;
		att_text.text = textSpeech.Append("Attack: <size=70>40</size>")
			.Append("\n\n火が龍の如く相手に襲い掛かる非常に威力が高い魔法である。").ToString();
		//des_text.text = "火が龍の如く相手に襲い掛かる非常に威力が高い魔法である";
		nimg.sizeDelta = new Vector2(100, 40);  
		mpimg.sizeDelta = new Vector2(128, 40);  
	}

	public void FlashEnter(){
		title.text = "<color=red><size=45>Flash</size></color>\n発音：flˈæʃ";
		textSpeech.Length = 0;
		att_text.text = textSpeech.Append("Attack: <size=70>60</size>")
			.Append("\n\n放電する術である。但し、使用には自身が持っているMP(チャクラ)の半分を消費する魔法である。").ToString();
		//att_text.text = "Attack <size=100>60</size>";
		//des_text.text = "放電する術である。但し、使用には自身が持っているMP(チャクラ)の半分を消費する魔法である";
		nimg.sizeDelta = new Vector2(250, 40);  
		mpimg.sizeDelta = new Vector2(192, 40);
	}

	public void LightEnter(){
		title.text = "<color=red><size=45>Flare</size></color>\n発音：フレアー";
		textSpeech.Length = 0;
		att_text.text = textSpeech.Append("Attack: <size=70>0</size>")
			.Append("\n\n夜間に目標を照明し観測するために使用する魔法である。発光する物体を空中に放ち、周囲を照らし視界が確保できる。").ToString();
		//att_text.text = "Attack <size=100>60</size>";
		//des_text.text = "放電する術である。但し、使用には自身が持っているMP(チャクラ)の半分を消費する魔法である";
		nimg.sizeDelta = new Vector2(180, 40);  
		mpimg.sizeDelta = new Vector2(256, 40);
	}
}
