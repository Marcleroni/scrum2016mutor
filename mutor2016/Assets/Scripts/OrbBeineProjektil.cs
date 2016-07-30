using UnityEngine;
using System.Collections;

public class OrbBeineProjektil : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col) {

		if (col.gameObject.tag == "EnemyFlying1") {
			EnemyFlying FlyScript = col.gameObject.GetComponent<EnemyFlying> ();
			FlyScript.Leben--;
		} 
		if (col.gameObject.tag == "EnemyThrowing1") {
			EnemySkelett SkelettScript = col.gameObject.GetComponent<EnemySkelett> ();
			SkelettScript.Leben--;
		}
		if (col.gameObject.tag == "EnemySliding1") {
			EnemySliding SlidingScript = col.gameObject.GetComponent<EnemySliding> ();
			SlidingScript.Leben--;
		}
		if (col.gameObject.tag == "Boss") {
			Boss1 BossScript = col.gameObject.GetComponent<Boss1> ();
			BossScript.BossLeben--;
		} 
		if (col.gameObject.tag == "Boss2") {
			Boss2 BossScript = col.gameObject.GetComponent<Boss2> ();
			BossScript.BossLeben--;
		} 

	}

	void OnTriggerStay2D(Collider2D col) {

		if (col.gameObject.tag == "EnemyFlying1") {
			EnemyFlying FlyScript = col.gameObject.GetComponent<EnemyFlying> ();
			FlyScript.Leben--;
		}
		if (col.gameObject.tag == "EnemyThrowing1") {
			EnemySkelett SkelettScript = col.gameObject.GetComponent<EnemySkelett> ();
			SkelettScript.Leben--;
		}
		if (col.gameObject.tag == "EnemySliding1") {
			EnemySliding SlidingScript = col.gameObject.GetComponent<EnemySliding> ();
			SlidingScript.Leben--;
		}
		if (col.gameObject.tag == "Boss") {
			Boss1 BossScript = col.gameObject.GetComponent<Boss1> ();
			BossScript.BossLeben--;
		} 
		if (col.gameObject.tag == "Boss2") {
			Boss2 BossScript = col.gameObject.GetComponent<Boss2> ();
			BossScript.BossLeben--;
		} 
	}

	public void DestroyOnRange () {
		Destroy(gameObject);
	} 

}
