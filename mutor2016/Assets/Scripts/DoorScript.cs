using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	Animator anim;
	public Animator animDoor;
	public float setTime;
	public float time;
	public bool countdown = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		time = setTime;
	}
	
	// Update is called once per frame
	void Update () {

		if (countdown == true) {
			time -= Time.deltaTime;
		}

		if (time < 0) {
			countdown = false;
			time = setTime;
			anim.SetBool ("On", false);
			animDoor.SetBool ("Open", false);
		}
	}

	void OnTriggerEnter2D (Collider2D col) {


		if (col.gameObject.tag == "Player") {
			time = setTime;
			countdown = true;
			anim.SetBool ("On", true);
			animDoor.SetBool ("Open", true);
		}
	}
}
