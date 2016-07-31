using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndbossPortal : MonoBehaviour {

	public GameObject gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag("GameManager");
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerStay2D(Collider2D col) {

		if (col.gameObject.tag == "Player") {
			if (Input.GetKeyDown (KeyCode.W)) {
				GameManager manager = gameManager.GetComponent<GameManager>();
				if (manager.SplitterCounter > 10) {
					SceneManager.LoadScene ("Endboss");
				} else if (manager.SplitterCounter < 11) {
				
				}
			}
		}
	}
}
