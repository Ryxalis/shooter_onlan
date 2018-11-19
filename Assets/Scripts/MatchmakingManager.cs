using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MatchmakingManager : MonoBehaviour {

	public bool readyToStart = false;
	[SerializeField] private int nbPlayersRequired = 2;
	private PlayerMatchmaking[] playersMatchmaking;

	void Update(){
		if (!readyToStart) {
			playersMatchmaking = FindObjectsOfType<PlayerMatchmaking> ();
			int nbPlayers = 0;
			for (int i = 0; i < playersMatchmaking.Length; ++i) {
				if (playersMatchmaking [i].isInMatchmaking) {
					nbPlayers++;
				}
			}
			if (nbPlayers >= nbPlayersRequired) {
				readyToStart = true;
			}
		}
	}
}
