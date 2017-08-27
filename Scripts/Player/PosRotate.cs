using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosRotate : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		//Quaternion offsetRot = GvrViewer.Instance.HeadPose.Orientation;
		Quaternion offsetRot = Camera.main.transform.rotation;
		offsetRot.eulerAngles = new Vector3 (0, offsetRot.eulerAngles.y, 0);
		//transform.localRotation = Quaternion.Lerp(transform.rotation, offsetRot, moveSpeed * Time.deltaTime);
		transform.rotation = offsetRot;
	}
}
