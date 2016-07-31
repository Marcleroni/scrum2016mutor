 using UnityEngine;
using System.Collections;

public class OrbBeineAktiv : MonoBehaviour {

	Animator anim;
	public Transform Spawn1;
	public Transform Spawn2;
	public GameObject prefab1;
	public GameObject Indikator;

	public AudioClip beine;
	public float beineVolume;
	AudioSource audio;
	//public GameObject prefab2;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

		PlayerController pc = gameObject.GetComponent<PlayerController>();

		if ((pc.shootBeine == true) && (anim.GetBool("Ground") == true)) {
			Shoot ();
		}


	}


	void Shoot () {

		Indikator = GameObject.FindGameObjectWithTag ("BeinIndikator");
		Destroy (Indikator);

		audio.PlayOneShot(beine,beineVolume);
		PlayerController pc = gameObject.GetComponent<PlayerController>();

		if (pc.facingRight) {
			GameObject welle1 = (GameObject)Instantiate(prefab1, Spawn1.position, Spawn1.rotation);
			GameObject welle2 = (GameObject)Instantiate(prefab1, Spawn2.position, Spawn2.rotation);
		}
		else if (!pc.facingRight) {
			GameObject welle1 = (GameObject)Instantiate(prefab1, Spawn1.position, Spawn2.rotation);
			GameObject welle2 = (GameObject)Instantiate(prefab1, Spawn2.position, Spawn1.rotation);
		}


		//Rigidbody2D rbwelle = welle.GetComponent<Rigidbody2D>();

		pc.shootBeine = false;
		anim.SetBool ("BeinAttacke", false);

	}

}
