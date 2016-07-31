using UnityEngine;
using System.Collections;

public class SplitterPopup : MonoBehaviour {

	public GameObject gameManager;
	public GameObject Ui;
	bool active = false;

	void Start () {
		gameManager = GameObject.FindGameObjectWithTag("GameManager");
	}

	void Update () {

		GameManager manager = gameManager.GetComponent<GameManager>();

		if ((manager.SplitterCounter == 1) && (!manager.splitterPopup)) {
			active = true;
			Ui.SetActive (true);
			Time.timeScale = 0;
			manager.splitterPopup = true;
		}

		if (active) {

			if (Input.GetKeyDown (KeyCode.O)) {
				Ui.SetActive (false);
				Time.timeScale = 1;
				manager.Items.Add (gameObject.tag);
				active = false;
				Destroy (gameObject);
			}
		}
	}


}
