using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	public float speedMultiplier = 1.0f;
	public float rotationMultiplier = 1.0f;
	public float bulletSpeedMultiplier = 1.0f;

	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.blue;
	}

	void Update()
	{
		if (!isLocalPlayer)
		{
			return;
		}

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit)){
			Vector3 look = hit.point;
			look.y = transform.position.y;

			float sign = Vector3.Cross (transform.forward, look - transform.position).normalized.y;
			float angle = sign * Mathf.Min(Vector3.Angle (transform.forward, look-transform.position), 100);
			transform.Rotate (0, angle, 0);
		}

		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f * speedMultiplier;
		var z = Input.GetAxis("Vertical")   * Time.deltaTime * 3.0f * speedMultiplier;

		// avoids the player to have a strange behaviour when hitting obstacles
		if (!Physics.SphereCast (transform.position, 0.49f, new Vector3(x, 0, 0), out hit, Mathf.Abs(x))) {
			transform.Translate (transform.InverseTransformVector (x, 0, 0));
		}
		if (!Physics.SphereCast (transform.position, 0.49f, new Vector3(0, 0, z), out hit, Mathf.Abs(z))) {
			transform.Translate (transform.InverseTransformVector (0, 0, z));
		}

		if(Input.GetMouseButtonDown(0))
		{
			CmdFire();
		}
	}

	[Command]
	void CmdFire(){
		var bullet = (GameObject)Instantiate (bulletPrefab,	bulletSpawn.position, bulletSpawn.rotation);
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6 * bulletSpeedMultiplier; // care, if the bullet is slower than the player, the player will destroy the bullet and take dammages.
		NetworkServer.Spawn (bullet);
		Destroy(bullet, 2.0f);
	}

}