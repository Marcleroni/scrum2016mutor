using UnityEngine;
using System.Collections;

public class KrallenHit : MonoBehaviour {

	public GameObject gameManager;

	// Use this for initializaction
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D (Collider2D col) {
		if (col.gameObject.tag == "Boss") {
			GameManager manager = gameManager.GetComponent<GameManager>();
			PlayerController control = GetComponentInParent<PlayerController>();
			if (manager.OrbKrallen && Input.GetKeyDown (control.ActionKey)) {
				Debug.Log ("BossHit");
				Boss1 BossScript = col.gameObject.GetComponent<Boss1> ();
				BossScript.BossLeben--;
			}
		}

	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Boss") {
			GameManager manager = gameManager.GetComponent<GameManager>();
			PlayerController control = GetComponentInParent<PlayerController>();
			if (manager.OrbKrallen && Input.GetKeyDown (control.ActionKey)) {
				Debug.Log ("BossHit");
				Boss1 BossScript = col.gameObject.GetComponent<Boss1> ();
				BossScript.BossLeben--;
			}
		}
	}

}
