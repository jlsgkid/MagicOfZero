using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTransform : MonoBehaviour {

	public GameObject transLight;	
	public float m_liveTime = 4.0f;
	public Transform mozhang;
	//public Transform orig;
	
	private IEnumerator LightTransGo(){
		while(m_liveTime >= 0){
			transLight.gameObject.transform.Translate(new Vector3(0, 4 * Time.deltaTime,0 ));
			m_liveTime -= Time.deltaTime;
			yield return null;
		}

	}
	
	private IEnumerator LightTransBack(){
		float dis = Vector3.Distance(transLight.gameObject.transform.position, mozhang.position);
		while(dis >5){
			transLight.gameObject.transform.rotation = Quaternion.Slerp(transLight.gameObject.transform.rotation, 
				Quaternion.LookRotation(mozhang.position-transLight.gameObject.transform.position), 10 * Time.deltaTime);
			//transLight.gameObject.transform.position += transLight.gameObject.transform.forward * 10 * Time.deltaTime;
			transLight.gameObject.transform.Translate(new Vector3(0, 10 * Time.deltaTime,0 ));
			yield return null;
			//transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
		}

	}
		
	public void StartBlink(){
		transLight.SetActive (true);
		StartCoroutine(LightTransGo ());
		Invoke ("DispLight", 4.0f);
		//DispLight();
	}

	private void DispLight(){
		transLight.gameObject.transform.localPosition = Vector3.zero;
		m_liveTime = 4.0f;
		transLight.SetActive (false);
	}

}
