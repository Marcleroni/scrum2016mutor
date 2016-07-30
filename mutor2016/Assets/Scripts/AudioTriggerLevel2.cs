using UnityEngine;
using System.Collections;

public class AudioTriggerLevel2 : MonoBehaviour {

	public GameObject cameraMain;
	public GameObject gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
		GameManager manager = gameManager.GetComponent<GameManager>();

		if (manager.Bosse.Contains ("Boss2")) {

				AudioManager audiom = cameraMain.GetComponent<AudioManager> ();
				if (audiom.audioS.clip != audiom.level1) {
					audiom.audioS.clip = audiom.level1;
					audiom.audioS.Play ();
				}
		}
	}

	void OnTriggerEnter2D (Collider2D col) {

		GameManager manager = gameManager.GetComponent<GameManager>();
		if (!manager.Bosse.Contains ("Boss2")) {

			if (col.gameObject.tag == "Player") {
				AudioManager audiom = cameraMain.GetComponent<AudioManager> ();
				if (audiom.audioS.clip != audiom.Boss1) {
					audiom.audioS.clip = audiom.Boss1;
					audiom.audioS.Play ();
				}
			}
		}
	}

	void OnTriggerExit2D (Collider2D col) {

			if (col.gameObject.tag == "Player") {
				AudioManager audiom = cameraMain.GetComponent<AudioManager> ();
				if (audiom.audioS.clip != audiom.level1) {
					audiom.audioS.clip = audiom.level1;
					audiom.audioS.Play ();
				}
			}
	}
}
