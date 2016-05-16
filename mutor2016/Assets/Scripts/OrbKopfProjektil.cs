using UnityEngine;
using System.Collections;

public class OrbKopfProjektil : MonoBehaviour {

	public Vector2 vel;
	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		vel = rb.velocity;

	//	if (rb.velocity.x < 0)									//Flip um Y-Achse
	//		Flip ();
	//	else if (rb.velocity.x > 0)
	//		Flip ();

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
		else if (col.gameObject.tag == "Gegner") {
			Destroy(gameObject);
		}

	}

	public void Flip () {

		//pc.facingRight = !pc.facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}


}
