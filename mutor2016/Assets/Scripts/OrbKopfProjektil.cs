﻿using UnityEngine;
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

}
