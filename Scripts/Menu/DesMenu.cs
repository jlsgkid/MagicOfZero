using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DesMenu : MonoBehaviour {

	[SerializeField]
	private GameObject magicMenu;
	private bool isShow = false;
	private bool isOn = false;
	public GameObject chongtu;
	private int num = 0;

	void Awake(){
		if(chongtu != null){
			chongtu.SetActive (true);
		}
	}
	void Update () {

		if(GvrController.AppButtonDown){
			num++;
			if (isShow) {
				Sequence mySequence = DOTween.Sequence ();  
				//mySequence.Append(transform.DOLocalMoveZ(-1.0f,2f));
				mySequence.PrependInterval (2)  
					.Insert (0, transform.DOScale (new Vector3 (0, 1, 0), mySequence.Duration ())); 
				isShow = false;
				Invoke ("SetGameObj", 2.5f);
			} else {
				magicMenu.SetActive (true);
				Sequence mySequence = DOTween.Sequence ();  
				//mySequence.Append(transform.DOLocalMoveZ(2f,2f));
				//mySequence.Append(transform.DOScale (new Vector3 (1, 1, 0.6f), 2.0f));
				mySequence.PrependInterval (2)  
					.Insert (0, transform.DOScale (new Vector3 (1, 1, 0.6f), mySequence.Duration ())); 
				isShow = true;
			}

		}

		if(chongtu != null){
			if (isShow) {
				chongtu.SetActive (false);
			}
		}

		//if(num != 0 && num % 2 == 0 ){
		if(num == 2 ){
			isOn = true;
		}

	}

	public bool isAppDown(){
		return this.isOn;
	}

	void SetGameObj(){
		magicMenu.SetActive (false);
		if(chongtu != null){
			chongtu.SetActive (true);
		}

	}
}
