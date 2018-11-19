using UnityEngine;
using System.Collections;

public class FaceCam : MonoBehaviour {

	void Update (){
		transform.LookAt (Camera.main.transform);
		transform.eulerAngles = new Vector3 (90, 0, 0);
	}
}