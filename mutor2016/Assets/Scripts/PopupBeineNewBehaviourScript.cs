using UnityEngine;
using System.Collections;

public class PopupBeineNewBehaviourScript : MonoBehaviour {

	public GameObject gameManager;
	public GameObject Ui;
	bool active = false;

	void Start () {
		gameManager = GameObject.FindGameObjectWithTag("GameManager");
		GameManager manager = gameManager.GetComponent<GameManager>();
		if (manager.Bosse.Contains(gameObject.tag)) {
			Destroy(gameObject);
		}
	}

	void Update () {

		if (active) {
		
			if (Input.GetKeyDown (KeyCode.O)) {
				Ui.SetActive (false);
				Time.timeScale = 1;
				GameManager manager = gameManager.GetComponent<GameManager>();
				manager.Items.Add (gameObject.tag);
				Destroy (gameObject);
			}
		}
	}

	void OnTriggerEnter2D (Collider2D col) {

		if (col.gameObject.tag == "Player") {
			active = true;
			Ui.SetActive (true);
			Time.timeScale = 0;
		}
	}

}


