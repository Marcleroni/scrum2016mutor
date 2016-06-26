using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	Animator anim;
	public Animator animDoor;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D col) {

		if (col.gameObject.tag == "Player") {
			anim.SetBool ("On", true);
			animDoor.SetBool ("Open", true);
		}
	}
}
