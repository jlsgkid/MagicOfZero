using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour {

	public static GamePlayManager _instance;
	private PlayerState playerSt;
	private bool isOver = false;
	private Snake snake;
	private Fox fox;
	private bool isClear = false;

	void Awake(){
		if (_instance == null) {
			_instance = this;
			//DontDestroyOnLoad (_instance.gameObject);
		} else if (this != _instance){
			Destroy (gameObject);
		}

		playerSt = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerState> ();
		//snake = GameObject.FindGameObjectWithTag ("Snake").GetComponent<Snake>();
		//fox = GameObject.FindGameObjectWithTag ("Fox").GetComponent<Fox>();
	}

	void Update(){
		//Debug.Log ("manager:---" + playerSt.GetCurrentLife ());
		if(playerSt != null && isOver == false){
			if(playerSt.GetCurrentLife() <= 0){
				//GameOver
				isOver = true;
				Invoke ("ChangeScene", 3.0f);
			}
		}
//		if(snake != null && snake.GetCurrentLife() <= 0){
//			isClear = true;
//			Invoke ("ChangeScene", 3.0f);
//		}
//		if(fox != null && fox.GetCurrentLife() <= 0){
//			isClear = true;
//			Invoke ("ChangeScene", 3.0f);
//		}
	}

	public bool GetIsOver(){
		return this.isOver;
	}
	public bool GetIsClear(){
		return this.isClear;
	}

	void ChangeScene(){
		//Application.LoadLevel("MenuUI");
		SceneManager.LoadScene ("MenuUI");
	}
}
