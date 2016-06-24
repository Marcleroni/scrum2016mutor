using UnityEngine;
using System.Collections;

public class WallTrigger : MonoBehaviour {

	SpriteRenderer rend;
	public GameObject wallClimbBoss;
	public GameObject cameraMain;

	// Use this for initialization
	void Start () {
		rend = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D col) {

		if (col.gameObject.tag == "Player") {
			rend.enabled = true;
			gameObject.GetComponent<BoxCollider2D> ().enabled = true;
			wallClimbBoss.GetComponent<BoxCollider2D> ().enabled = true;
			AudioManager audiom = cameraMain.GetComponent<AudioManager> ();
			if (audiom.audioS.clip != audiom.Boss1) {
				audiom.audioS.clip = audiom.Boss1;
				audiom.audioS.Play ();
			}
		}
	}

}
