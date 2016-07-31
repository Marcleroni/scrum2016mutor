using UnityEngine;
using System.Collections;

public class OrbFügelProjektil : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col) {

		if (col.gameObject.tag == "Terrain") {
			Destroy (gameObject);
		} else if (col.gameObject.tag == "EnemyFlying1") {
			EnemyFlying FlyScript = col.gameObject.GetComponent<EnemyFlying> ();
			FlyScript.Leben--;
			Destroy (gameObject);
		} else if (col.gameObject.tag == "EnemyThrowing1") {
			EnemySkelett SkelettScript = col.gameObject.GetComponent<EnemySkelett> ();
			SkelettScript.Leben--;
			Destroy (gameObject);
		} else if (col.gameObject.tag == "EnemySliding1") {
			EnemySliding SlidingScript = col.gameObject.GetComponent<EnemySliding> ();
			SlidingScript.Leben--;
			Destroy (gameObject);
		} else if (col.gameObject.tag == "Boss") {
			Boss1 BossScript = col.gameObject.GetComponent<Boss1> ();
			BossScript.BossLeben--;
			Destroy (gameObject);
		} else if (col.gameObject.tag == "Boss2") {
			Boss2 BossScript = col.gameObject.GetComponent<Boss2> ();
			BossScript.BossLeben--;
			Destroy (gameObject);
		} else if (col.gameObject.tag == "Boss3") {
			Destroy (gameObject);
		} 

	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Boss3") {
			Boss3 BossScript = col.gameObject.GetComponent<Boss3> ();
			BossScript.BossLeben--;
			Destroy (gameObject);
		}
	}

	public void DestroyOnRange () {
		Destroy(gameObject);
	} 

}
