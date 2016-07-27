using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PortalLevel2 : MonoBehaviour {

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
			if (Input.GetKeyDown (KeyCode.Return)) {
				GameManager manager = gameManager.GetComponent<GameManager>();
				manager.fromLevel = SceneManager.GetActiveScene ().buildIndex;
				SceneManager.LoadScene ("Level2");
			}
		}
	}
}
