using UnityEngine;
using System.Collections;

public class Boss1Hit : MonoBehaviour {

	public GameObject gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D (Collider2D col) {

		Boss1 bossScript = GetComponentInParent<Boss1>();

		if (col.gameObject.tag == "Player") {
			GameManager manager = gameManager.GetComponent<GameManager>();
			if (bossScript.canHit) {
				Debug.Log ("PlayerHit");
				manager.Leben--;
				bossScript.canHit = false;
			}
		}

	}

	void OnTriggerEnter2D (Collider2D col) {

		Boss1 bossScript = GetComponentInParent<Boss1>();

		if (col.gameObject.tag == "Player") {
			GameManager manager = gameManager.GetComponent<GameManager>();
			if (bossScript.canHit) {
				Debug.Log ("PlayerHit");
				manager.Leben--;
				bossScript.canHit = false;
			}
		}
	}
}
