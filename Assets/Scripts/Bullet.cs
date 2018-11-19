using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	void OnCollisionEnter(Collision coll)
	{
		var hit = coll.gameObject;
		if (hit.GetComponent<Player> ()) {
			hit.GetComponent<Player> ().TakeDammages (10);
		}
		else if (hit.GetComponentInParent<Player> ()) {
			hit.GetComponentInParent<Player> ().TakeDammages (10);
		}
		Destroy(gameObject);
	}
}