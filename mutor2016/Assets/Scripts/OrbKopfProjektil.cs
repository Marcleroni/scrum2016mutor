using UnityEngine;
using System.Collections;

public class OrbKopfProjektil : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D col) {

		if (col.gameObject.tag == "Terrain") {
			//gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
			Destroy(gameObject);
		}
		else if (col.gameObject.tag == "LaserDestruction") {
			Destroy(col.gameObject);
			Destroy(gameObject);
		}
		else if (col.gameObject.tag == "EnemyFlying1") {
			EnemyFlying FlyScript = col.gameObject.GetComponent<EnemyFlying> ();
			FlyScript.Leben--;
			Destroy (gameObject);
		}

	}

	public void DestroyOnRange () {
		Destroy(gameObject);
	} 


}
