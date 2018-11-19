using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour {

	[SerializeField] private AudioSource mainMusic = null;
	private bool isGameStarted = false;
	private MatchmakingManager matchManager;

	void Start(){
		matchManager = FindObjectOfType<MatchmakingManager> ();
	}

	void Update(){
		if (!isGameStarted && matchManager.readyToStart) {
			StartGame ();
		}
	}

	void StartGame(){
		isGameStarted = true;
		FindObjectOfType<EnemySpawner> ().GenerateEnnemies (); // generate ennemies
		mainMusic.Play();
	}

}
