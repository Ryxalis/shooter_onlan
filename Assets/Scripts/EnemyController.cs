using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public float speedMultiplier = 1.0f;

	void Start () {
		
	}

	void Update () {
		var z = Time.deltaTime * 3.0f * speedMultiplier;
		transform.Translate(0, 0, z);
	}

	void OnCollisionStay(Collision coll){
		if (coll.collider.name == "Ground") {
			return;
		}
		var newRotation = Quaternion.Euler (0.0f, Random.Range (0, 360), 0.0f);
		transform.rotation = newRotation;
	}
}
