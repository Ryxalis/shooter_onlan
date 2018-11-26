using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour {

	[SerializeField] private AudioSource mainMusic = null;
	[SerializeField] private GameObject mainMenuCanvas = null;
	[SerializeField] private NetworkManager netManager = null;
	[SerializeField] private NetworkManagerHUD netManagerHUD = null;
	private bool isGameStarted = false;
	private bool isMultiplayer = false;
	private MatchmakingManager matchManager;

	public bool IsMultiplayer { get {return isMultiplayer;} }
	public bool IsGameStarted { get {return isGameStarted;} }

	void Start(){
		matchManager = FindObjectOfType<MatchmakingManager> ();
	}

	void Update(){
		if (!isGameStarted && isMultiplayer && matchManager.readyToStart) {
			StartGame ();
		}
	}

	void StartGame(){
		isGameStarted = true;
		FindObjectOfType<EnemySpawner> ().GenerateEnnemies (); // generate ennemies
		mainMusic.Play();
	}

	//** Functions used in the main menu **//

	public void MenuStartSinglePlayer(){
		mainMenuCanvas.SetActive (false);
		netManager.StartHost ();
		netManagerHUD.showGUI = false;
		matchManager.gameObject.SetActive (false);
		StartGame ();
	}

	public void MenuStartMultiPlayer(){
		mainMenuCanvas.SetActive (false);
		isMultiplayer = true;
	}

	public void MenuQuit(){     
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}

}
