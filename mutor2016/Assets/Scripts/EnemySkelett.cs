﻿using UnityEngine;
using System.Collections;

public class EnemySkelett : MonoBehaviour {

	Animator anim;
	public GameObject boneStick;
	public Transform spawn;
	public Transform target;
	public float distance;
	public float throwDist = 5f;
	public int Leben = 1;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		distance = Vector2.Distance(transform.position, target.position);

		if (throwDist > distance) {
			anim.SetBool ("Throw", true);
		} else if (throwDist < distance) {
			anim.SetBool ("Throw", false);		
		}

		if (Leben < 1) {
			anim.SetBool ("Alive", false);
		}

	}

	public void Shoot () {

		PlayerController pc = gameObject.GetComponent<PlayerController> ();

		GameObject stick = (GameObject)Instantiate (boneStick, spawn.position, spawn.rotation);

	}

	public void Death() {
		Destroy (gameObject);
	}
}
