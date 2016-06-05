using UnityEngine;
using System.Collections;

public class OrbFügelProjektil : MonoBehaviour {

	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col) {

		if (col.gameObject.tag == "Terrain") {
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
