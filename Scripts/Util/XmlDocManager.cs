using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class XmlDocManager : MonoBehaviour {

	[SerializeField]
	private string fileName = "curses.xml";
	//The name of the top layer
	private static string prt_curse = "curse_config";
	public List<string> allCurses = null;

	// Use this for initialization

	public List<string> GetXmlList(){
		return this.allCurses;
	}

	private string GetFilePath(){
		
		string filePath = "";
		#if UNITY_EDITOR
		//filePath = "file://" +Application.dataPath +"/StreamingAssets/";
		filePath = "file://" +Application.streamingAssetsPath +"/";
		#elif UNITY_IPHONE && !UNITY_EDITOR
		filePath = Application.dataPath +"/Raw/";
		#elif UNITY_ANDROID && !UNITY_EDITOR
		//filePath = "jar:file://" + Application.dataPath + "!/assets/"+"/";
		//filePath = Application.dataPath + "!assets"+"/";
		filePath = Application.streamingAssetsPath +"/";
		#endif
		return filePath;
	}
	
    IEnumerator loadXML() {
		string filePath = GetFilePath ();
		Debug.Log (filePath);
		WWW www = new WWW(filePath + fileName);
		while (!www.isDone) {
			yield return www;
			Debug.Log("Text:" + www.text);
			ReadXML (www, prt_curse);
		}
	}

	private void ReadXML(WWW www, string prtName){
		if (allCurses == null) {
			allCurses = new List<string> ();
		}
			
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.LoadXml(www.text);

		XmlNodeList nodeList=xmlDoc.SelectSingleNode(prtName).ChildNodes;
		foreach(XmlElement xe in nodeList){
			string id = xe.GetAttribute("cid");
			string value = xe.GetAttribute("cvalue");
			Debug.Log ("cid:"+id+"cvalue:"+value);
			allCurses.Add(value);
		}
	}
	 
	void Start(){
		StartCoroutine (loadXML());
	}
}
