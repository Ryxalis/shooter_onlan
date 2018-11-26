using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
	private Transform cameraTransform;
	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	public float speedMultiplier = 1.0f;
	public float bulletSpeedMultiplier = 1.0f;

	private Vector3 clickPosition;

	public override void OnStartLocalPlayer()
	{
		clickPosition = transform.position;
		cameraTransform = FindObjectOfType<MainCameraFollowPlayer> ().transform;
		GetComponent<MeshRenderer>().material.color = Color.blue;
		FindObjectOfType<MainCameraFollowPlayer>().ChoosePlayer (transform);
	}

	void Update()
	{
		if (!isLocalPlayer)
		{
			return;
		}

		int layerMask = 1 << 9; // cast rays only against colliders in layer 9.
		RaycastHit hit;
		Vector3 look = Vector3.zero;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)){
			look = hit.point;
			look.y = transform.position.y;
			if (Input.GetMouseButtonDown (0)) {
				clickPosition = look;
			}

			float sign = Vector3.Cross (transform.forward, look - transform.position).normalized.y;
			float angle = sign * Mathf.Min(Vector3.Angle (transform.forward, look-transform.position), 100);
			transform.Rotate (0, angle, 0);
		}

		//var x = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f * speedMultiplier;
		//var z = Input.GetAxis("Vertical")   * Time.deltaTime * 3.0f * speedMultiplier;
		Vector3 delta = clickPosition - transform.position;
		if (delta.magnitude < 1.5*speedMultiplier * 0.01f) { //speedMultiplier * 0.01f is supposed to be the distance run in 1 frame, so we secure a 1.5 threshold to avoid the non-convergence.
			clickPosition = transform.position;
			delta = Vector3.zero;
		}

		transform.Translate (transform.InverseTransformVector(delta.normalized * speedMultiplier * 0.01f));

		// avoids the player to have a strange behaviour when hitting obstacles
		/*if (!Physics.SphereCast (transform.position, 0.49f, cameraTransform.right * x, out hit, Mathf.Abs(x))) {
			transform.Translate (transform.InverseTransformVector(cameraTransform.right * x));//(transform.InverseTransformVector (x, 0, 0));
		}
		if (!Physics.SphereCast (transform.position, 0.49f, cameraTransform.forward * z, out hit, Mathf.Abs(z))) {
			transform.Translate (transform.InverseTransformVector(cameraTransform.forward * z));// (transform.InverseTransformVector (0, 0, z));
		}*/

		/*if(Input.GetMouseButtonDown(0))
		{
			CmdFire();
		}*/
	}

	[Command]
	void CmdFire(){
		var bullet = (GameObject)Instantiate (bulletPrefab,	bulletSpawn.position, bulletSpawn.rotation);
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6 * bulletSpeedMultiplier; // care, if the bullet is slower than the player, the player will destroy the bullet and take dammages.
		NetworkServer.Spawn (bullet);
		Destroy(bullet, 2.0f);
	}

}