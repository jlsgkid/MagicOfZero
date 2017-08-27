using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class circleProcess2 : MonoBehaviour {
	
	[SerializeField]
	float speed;
	
	[SerializeField]
	Transform process;
	
	[SerializeField]
	Transform indicator;

	public int targetProcess{ get; set;}
	public float currentAmout = 0;
	public int GetCurrentAmout(){
		return (int)this.currentAmout;
	}
	public void SetCurrentAmout(int num){
		this.currentAmout = num;
	}
	// Use this for initialization
	void Start () {
		targetProcess = 100;

	}

	// Update is called once per frame
	void Update () {
		if(currentAmout >=100){
			this.gameObject.SetActive (false);
		}
		if (currentAmout < targetProcess) {
			//Debug.Log("currentAmount:" + currentAmout.ToString());
			currentAmout += speed;
			if(currentAmout > targetProcess)
				currentAmout = targetProcess;
			indicator.GetComponent<Text>().text = ((int)currentAmout).ToString() + "%";
			process.GetComponent<Image>().fillAmount = 50;
		}


	}



	public void ChangeManaCir(int mana){
		this.currentAmout -= mana;
	}
	
	public void SetTargetProcess(int target)
	{
		if(target >= 0 && target <= 100)
			currentAmout = target;
	}

}
