using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class Fox : MonoBehaviour {

	//ステータス
	public enum State
	{
		IDLE,
		WALK,
		ATTACK,
		DIE
	}
	public State state;
	//アニメ
	private Animation anim;
	//生命
	[SerializeField]
	private int life = 60;
	public int GetCurrentLife(){
		return this.life;
	}
	public Slider xuetiao;
	private Transform player;
	private float timer = 0;
	private PlayerState ps;
	public float walkSpeed = 3.0f;
	//プレイヤーとの距離
	private float dis = 0.0f;
	//Player HP
	[SerializeField] private Text hp;
	StringBuilder hpStr = new StringBuilder ();
	[SerializeField] private bool isGazeIn = false;
	private PlayerMove pm;

	// Use this for initialization
	void Awake () {
		
		state = State.IDLE;
		anim = this.GetComponent<Animation> ();
		GameObject playerObj =  GameObject.FindGameObjectWithTag ("Player") as GameObject;
		player = playerObj.transform;
		ps = playerObj.GetComponent<PlayerState> ();
		dis = Vector3.Distance(transform.position, player.position);
		pm = playerObj.GetComponent<PlayerMove> ();
	}
	
	// Update is called once per frame
	void Update () {

		if(GamePlayManager._instance.GetIsOver()){
			state = State.IDLE;
			hpStr.Length = 0;
			hpStr = hpStr.Append ("HP: 0%");
		}
		AnimationControl();
		xuetiao.value = this.life;
		timer += Time.deltaTime;
		if (this.life <= 0) {
			state = State.DIE;
			xuetiao.gameObject.SetActive (false);
			hp.gameObject.SetActive (false);
			Destroy (this.gameObject, 2.0f);
			return;
		}
		dis = Vector3.Distance(transform.position, player.position);
		//Debug.Log (dis);
		if (dis < 15 && dis > 2.3) {
			state = State.WALK;
			xuetiao.gameObject.SetActive (true);
			WalkToPlay ();
		} else if (dis < 2.3) {
			state = State.ATTACK;
		} 

		if (state == State.ATTACK && !GamePlayManager._instance.GetIsOver ()) {
			//狼の攻撃間
			if (timer > 1.0f) {
				//プレイヤーに攻撃する
				if (hp != null) {
					hp.gameObject.SetActive (true);
					hpStr.Length = 0;
					hpStr = hpStr.Append ("HP: ")
						.Append (ps.GetCurrentLife ())
						.Append ("%");
					hp.text = hpStr.ToString ();

				}
				ps.GetDamage (5);
				ps.xuetiaoFadeIn ();
				//player speed ++ 
				pm.moveSpeed = 4f;
				Invoke ("ChangPlSpeeed", 2.0f);
				timer = 0;
			}
		} 
	}
	void ChangPlSpeeed(){
		pm.moveSpeed = 1.5f;
	}

	private void WalkToPlay(){
		transform.rotation = Quaternion.Slerp(transform.rotation, 
			Quaternion.LookRotation(player.position-transform.position), walkSpeed * Time.deltaTime);
		transform.position += transform.forward * walkSpeed * Time.deltaTime;
		transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
	}

	public void GetDamage(int damage){
		if (this.life > 0) {
			this.life -= damage;
		} else {
			state = State.DIE;
		}
	}

	private void AnimationControl(){
		switch (state) {
		case State.IDLE:
			anim.Play ("idleLookAround");
			break;
		case State.WALK:
			anim.Play ("run");
			break;
		case State.ATTACK:
			//anim.Play ("agressiveJumpBite");
			anim.Play ("standAgressiveBite");
			break;
		case State.DIE:
			anim.Play ("death");
			break;
		}
	}

	public void ContrlPointerEnter(){
		isGazeIn = true;
	}
	public void ContrlPointerExit(){
		isGazeIn = false;
	}
	public bool GetIsGazeIn(){
		return isGazeIn;
	}


}
