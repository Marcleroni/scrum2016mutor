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

		if (col.gameObject.tag == "Boss2") {
			GameManager manager = gameManager.GetComponent<GameManager>();
			PlayerController control = GetComponentInParent<PlayerController>();
			if (manager.OrbKrallen && Input.GetKeyDown (control.ActionKey)) {
				Debug.Log ("BossHit");
				Boss2 BossScript = col.gameObject.GetComponent<Boss2> ();
				BossScript.BossLeben--;
			}
		}

		if (col.gameObject.tag == "EnemyFlying1") {
			GameManager manager = gameManager.GetComponent<GameManager>();
			PlayerController control = GetComponentInParent<PlayerController>();
			EnemyFlying FlyScript = col.gameObject.GetComponent<EnemyFlying> ();
			if (manager.OrbKrallen && Input.GetKeyDown (control.ActionKey) && FlyScript.alive) {
				FlyScript.Leben--;
			}
		}

		if (col.gameObject.tag == "EnemyThrowing1") {
			GameManager manager = gameManager.GetComponent<GameManager>();
			PlayerController control = GetComponentInParent<PlayerController>();
			EnemySkelett SkelettScript = col.gameObject.GetComponent<EnemySkelett> ();
			if (manager.OrbKrallen && Input.GetKeyDown (control.ActionKey)) {
				SkelettScript.Leben--;
			}
		}

		if (col.gameObject.tag == "EnemySliding1") {
			GameManager manager = gameManager.GetComponent<GameManager>();
			PlayerController control = GetComponentInParent<PlayerController>();
			EnemySliding SlidingScript = col.gameObject.GetComponent<EnemySliding> ();
			if (manager.OrbKrallen && Input.GetKeyDown (control.ActionKey)) {
				SlidingScript.Leben--;
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

		if (col.gameObject.tag == "Boss2") {
			GameManager manager = gameManager.GetComponent<GameManager>();
			PlayerController control = GetComponentInParent<PlayerController>();
			if (manager.OrbKrallen && Input.GetKeyDown (control.ActionKey)) {
				Debug.Log ("BossHit");
				Boss2 BossScript = col.gameObject.GetComponent<Boss2> ();
				BossScript.BossLeben--;
			}
		}

		if (col.gameObject.tag == "EnemyFlying1") {
			GameManager manager = gameManager.GetComponent<GameManager>();
			PlayerController control = GetComponentInParent<PlayerController>();
			EnemyFlying FlyScript = col.gameObject.GetComponent<EnemyFlying> ();
			if (manager.OrbKrallen && Input.GetKeyDown (control.ActionKey) && FlyScript.alive) {
				FlyScript.Leben--;
			}
		}

		if (col.gameObject.tag == "EnemyThrowing1") {
			GameManager manager = gameManager.GetComponent<GameManager>();
			PlayerController control = GetComponentInParent<PlayerController>();
			EnemySkelett SkelettScript = col.gameObject.GetComponent<EnemySkelett> ();
			if (manager.OrbKrallen && Input.GetKeyDown (control.ActionKey)) {
				SkelettScript.Leben--;
			}
		}

		if (col.gameObject.tag == "EnemySliding1") {
			GameManager manager = gameManager.GetComponent<GameManager>();
			PlayerController control = GetComponentInParent<PlayerController>();
			EnemySliding SlidingScript = col.gameObject.GetComponent<EnemySliding> ();
			if (manager.OrbKrallen && Input.GetKeyDown (control.ActionKey)) {
				SlidingScript.Leben--;
			}
		}
	}

}
