using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

	public int life = 100;
	public VRCameraFade vfade;
	public Color fadeColor = Color.red; 

	// Use this for initialization

	public void GetDamage(int damage){
		if (this.life > 0) {
			this.life -= damage;
		} else {
			this.life = 0;
		}
	}

	public void xuetiaoFadeIn(){
		//Camera FadeIn
		vfade.m_FadeColor = fadeColor;
		vfade.StartFadeIn (true);
	}

	public int GetCurrentLife(){
		return life;
	}

}
