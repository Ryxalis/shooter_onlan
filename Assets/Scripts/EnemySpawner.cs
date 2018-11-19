using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour {

	public Transform terrain;
	private float maxX;
	private float maxZ;

	public GameObject enemyPrefab;
	public int numberOfEnemies;

	void Start(){
		maxX = terrain.localScale.x;
		maxZ = terrain.localScale.z;
	}

	//public override void OnStartServer()
	public void GenerateEnnemies()
	{
		RaycastHit hit;
		float m_radius = enemyPrefab.GetComponent<CapsuleCollider> ().radius;
		int effectiveEnemies = 0;
		int iter = 0;
		int maxIter = 1000;
		while(effectiveEnemies < numberOfEnemies && iter < maxIter)
		{
			iter++;
			var spawnPosition = new Vector3(Random.Range(-maxX, maxX), 1.0f, Random.Range(-maxZ, maxZ));
			if (!Physics.SphereCast (spawnPosition+5*Vector3.up, m_radius, -transform.up, out hit, 4.5f)) {
				var spawnRotation = Quaternion.Euler (0.0f, Random.Range (0, 360), 0.0f);
				var enemy = (GameObject)Instantiate (enemyPrefab, spawnPosition, spawnRotation);
				NetworkServer.Spawn (enemy);
				effectiveEnemies++;
			}
		}
		if (iter >= maxIter) {
			Debug.Log("Some enemies did not spawn.");
		}
	}
}