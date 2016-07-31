using UnityEngine;
using System.Collections;

public class OrbKopfProjektil : MonoBehaviour {

	Animator target;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D col) {

		if (col.gameObject.tag == "Terrain") {
			//gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
			Destroy (gameObject);
		} else if (col.gameObject.tag == "LaserDestruction") {
			target = col.gameObject.GetComponent<Animator> ();
			target.SetBool ("Burned", true);
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
		} else if (col.gameObject.tag == "Endboss") {
			Endboss BossScript = col.gameObject.GetComponent<Endboss> ();
			BossScript.BossLeben--;
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
