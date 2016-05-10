using UnityEngine;
using System.Collections;

public class OrbKopf : MonoBehaviour {

	Rigidbody2D rb;
	public GameObject laserPrefab;
	public Transform laserStart;
	public LayerMask ToHit;
	public int laserSpeed = 10;
	public int laserMax = 1;	//Lasers at a Time
	public int laserCount;
	bool laserShootable;
	Animator anim;
	//public AudioClip bombThrow;
	//public float throwVolume;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {

		if (laserCount < laserMax) {
			laserShootable = true;
		}
		else {
			laserShootable = false;
		}

		if (Input.GetKeyDown (KeyCode.P) && laserShootable) {

			//AudioSource.PlayClipAtPoint (bombThrow, bombStart.position, throwVolume);
			//anim.SetTrigger ("Throw");
			Shoot();
		}

	}

	void Shoot () {

		PlayerController pc = gameObject.GetComponent<PlayerController>();

		GameObject laser = (GameObject)Instantiate(laserPrefab, laserStart.position, laserStart.rotation);
		Rigidbody2D rblaser = laser.GetComponent<Rigidbody2D>();

		Vector2 direction = rb.transform.right;
		direction.Normalize();

		if (pc.facingRight) {
			rblaser.velocity = (direction) * laserSpeed * Time.deltaTime;
		}
		else if (!pc.facingRight) {
			rblaser.velocity = (-direction) * laserSpeed * Time.deltaTime;
		}
			
		Physics2D.IgnoreCollision(rblaser.GetComponent<Collider2D>(),  GetComponent<Collider2D>());

		laserCount++;		//Counter für Projektile
	}
}
