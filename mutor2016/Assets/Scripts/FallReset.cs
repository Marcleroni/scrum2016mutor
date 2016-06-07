using UnityEngine;
using System.Collections;

public class FallReset : MonoBehaviour {

	public Transform player;
	public Transform cam;
	public Transform spawn;
	public Transform cameraSpawn;

	void OnTriggerEnter2D (Collider2D col) {

		if (col.gameObject.tag == "Player") {
			Debug.Log ("test");
			player.position = spawn.position;
			cam.position = cameraSpawn.position;
			}
		}
}
