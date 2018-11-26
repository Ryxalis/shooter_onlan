using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraFollowPlayer : MonoBehaviour {

	public float rotateSpeedMultiplier = 1f;
	private Transform player;
	private GameManager gameManager;

	void Start () {
		gameManager = FindObjectOfType<GameManager> ();
	}
	
	void Update () {
		if (gameManager.IsGameStarted && player) {
			transform.position = player.position;
			if (Input.GetMouseButton (1)) {
				transform.Rotate(0, Input.GetAxis ("Mouse X") * rotateSpeedMultiplier, 0);
			}
		}
	}

	public void ChoosePlayer(Transform playerTransform){
		player = playerTransform;
	}
}
