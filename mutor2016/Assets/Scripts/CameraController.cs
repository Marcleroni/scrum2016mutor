 using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	Vector2 velocity;
	public float smoothTimeX;
	public float smoothTimeY;

	public GameObject player;

	public bool bounds;
	public Vector3 min;
	public Vector3 max;
	public float saver;

	public bool camFollow = true;
	public float valueUp = 2;

	// Use this for initialization
	void Start () {
		//player = GameObject.FindGameObjectWithTag("Player");
	}

	void FixedUpdate () {

	//	if (Input.GetAxis ("Vertical") > 0) {		//CAM Move Up On W Press
	//		camFollow = false;
	//	} else {
	//		camFollow = true;
	//	}

		if (camFollow) {
			saver = transform.position.y;
		}

	}

	// Update is called once per frame
	void LateUpdate () {

		if (camFollow) {
			float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
			float posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);
			transform.position = new Vector3 (posX, posY, transform.position.z);
		} else if (!camFollow) {
			float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
			float posY = Mathf.SmoothDamp (transform.position.y,saver + valueUp, ref velocity.y, smoothTimeY);
			transform.position = new Vector3 (posX, posY, transform.position.z);
		}

		if (bounds) {

			transform.position = new Vector3 (Mathf.Clamp(transform.position.x, min.x, max.x), Mathf.Clamp(transform.position.y, min.y, max.y), Mathf.Clamp(transform.position.z, min.z, max.z));

		}

	}
}
