using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FallReset : MonoBehaviour {

	public GameObject gameManager;
	public int storeLife;
	public int storeTest;

	void Start () {
		gameManager = GameObject.FindGameObjectWithTag("GameManager");
		GameManager manager = gameManager.GetComponent<GameManager>();
		storeLife = manager.lebenTotal-1;
	}

	void OnTriggerEnter2D (Collider2D col) {

		if (col.gameObject.tag == "Player") {
			GameManager manager = gameManager.GetComponent<GameManager>();
				Scene scene = SceneManager.GetActiveScene();
				SceneManager.LoadScene(scene.name);
			manager.lebenTotal = storeLife;
			}
		}
}
