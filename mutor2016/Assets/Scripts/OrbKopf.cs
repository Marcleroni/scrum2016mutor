using UnityEngine;
using System.Collections;

public class OrbKopf : MonoBehaviour {

	Rigidbody2D rb;
	public GameObject laserPrefab;
	public Transform laserStart;
	public LayerMask ToHit;
	public float laserSpeed = 1.65f;
	public int laserMax = 1;	//Lasers at a Time
	public int laserCount;
	bool laserShootable;
	Animator anim;
	public GameObject kopfAnim;
	Animator animKopf;
	//public AudioClip bombThrow;
	//public float throwVolume;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		animKopf = kopfAnim.GetComponent<Animator>();
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
			animKopf.SetTrigger ("KopfLaser");
			//AudioSource.PlayClipAtPoint (bombThrow, bombStart.position, throwVolume);
			//anim.SetTrigger ("Throw");
			Shoot();
		}

	}

	void Shoot () {

		PlayerController pc = gameObject.GetComponent<PlayerController>();

		GameObject laser = (GameObject)Instantiate(laserPrefab, laserStart.position, laserStart.rotation);
		Rigidbody2D rblaser = laser.GetComponent<Rigidbody2D>();

		if (pc.facingRight) {
			rblaser.velocity = new Vector2(laserSpeed, 0);
		}
		else if (!pc.facingRight) {
			rblaser.velocity = new Vector2(-laserSpeed, 0);
			Vector3 theScale = rblaser.transform.localScale;
			theScale.x *= -1;
			rblaser.transform.localScale = theScale;
		}
			
		Physics2D.IgnoreCollision(rblaser.GetComponent<Collider2D>(),  GetComponent<Collider2D>());

		laserCount++;		//Counter für Projektile
	}
}
