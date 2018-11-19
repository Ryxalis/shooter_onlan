using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerMatchmaking : NetworkBehaviour {

	[SyncVar] public bool isInMatchmaking = false;
	private PlayerController pController;
	[SerializeField] private GameObject buttonMatchmaking = null;
	private MatchmakingManager matchManager;

	void Start(){
		if (isLocalPlayer) {
			matchManager = FindObjectOfType<MatchmakingManager> ();
			pController = GetComponent<PlayerController> ();
			pController.enabled = false;
		} else {
			buttonMatchmaking.SetActive (false);
		}
	}

	void Update(){
		if (isLocalPlayer) {
			if (matchManager.readyToStart) {
				StartGame ();
			}
		}
	}

	public void JoinMatch(){
		if (!isLocalPlayer) {
			return;
		}
		if (!isInMatchmaking) {
			buttonMatchmaking.GetComponentInChildren<Text> ().text = "Waiting for player 2...";
			buttonMatchmaking.GetComponentInChildren<Button> ().interactable = false;
			isInMatchmaking = true;
			if (!isServer) {
				CmdIsInMatchmaking ();
			}
		}
	}

	[Command]
	void CmdIsInMatchmaking(){
		isInMatchmaking = true;
	}

	void StartGame(){
		if (isLocalPlayer) {
			buttonMatchmaking.SetActive (false);
			pController.enabled = true;
		}
	}
}
