using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

	public const int maxHealth = 100;

	[SyncVar(hook = "OnChangeHealth")] // OnChangeHealth is invoked on the server plus all the clients whenever the variable changes.
	private int currenHealth = maxHealth;
	private NetworkStartPosition[] spawnPoints;

	[SerializeField] private RectTransform healthBar = null;
	[SerializeField] private bool destroyOnDeath = false;

	void Start(){
		if (isLocalPlayer) {
			spawnPoints = FindObjectsOfType<NetworkStartPosition> ();
		}
	}

	public void TakeDammages(int dammages){
		if (!isServer) {
			return;
		}
		currenHealth = Mathf.Max (0, currenHealth - dammages);
		if (currenHealth <= 0) {
			if (destroyOnDeath) {
				Destroy (gameObject);
			} else {
				RpcRespawn ();
				currenHealth = maxHealth;
			}
		}
	}

	public void OnChangeHealth(int health){
		healthBar.sizeDelta = new Vector2 (health, healthBar.sizeDelta.y);
	}

	[ClientRpc]
	void RpcRespawn(){
		if (isLocalPlayer) {
			Vector3 spawnPoint = Vector3.zero;
			if (spawnPoints != null && spawnPoints.Length > 0)
			{
				spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
			}
			transform.position = spawnPoint;
		}
	}
}
