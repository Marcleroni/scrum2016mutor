using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FallReset : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D col) {

		if (col.gameObject.tag == "Player") {
				Scene scene = SceneManager.GetActiveScene();
				SceneManager.LoadScene(scene.name);
			}
		}
}
