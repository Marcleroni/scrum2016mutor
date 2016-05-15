using UnityEngine;
using System.Collections;

public class EnemyFlying : MonoBehaviour {

	Rigidbody2D rb;
	public Transform target;
	public float speed = 1;
	public bool facingRight = true;
	public float move;
	bool alive = true;
	bool attack = false;
	Animator anim;
	public float distance;
	public float followDistance;
	public float chargeDistance;
	public bool canCharge = true;
	public float chargeSpeed = 150;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	void FixedUpdate () {


		distance = Vector2.Distance(transform.position, target.position);

		if (alive && !attack && (distance < followDistance) && (distance > chargeDistance)) {
			
			canCharge = true;
			transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

			move = transform.position.x - target.position.x;

			if (move > 0 && !facingRight)									//Flip um Y-Achse
				Flip ();
			else if (move < 0 && facingRight)
				Flip ();	
		}
		else if (!alive) {
			anim.SetBool ("Alive", false);
		}
		else if (alive && !attack && (distance < followDistance) && (distance < chargeDistance) && canCharge) {

			canCharge = false;
			anim.SetBool ("Charge", true);
			move = transform.position.x - target.position.x;

			if (move > 0 && !facingRight)									//Flip um Y-Achse
				Flip ();
			else if (move < 0 && facingRight)
				Flip ();
		}



	}

	public void Flip () {

		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}

	void OnCollisionEnter2D(Collision2D col) {

		if (col.gameObject.tag == "Projektil") {
			alive = false;
			rb.velocity = new Vector2 (0, 0);
			if (!facingRight)									
				transform.Rotate (0,-180,146);
			else if (facingRight)
				transform.Rotate (0,-180,-146);
		}
		else if (col.gameObject.tag == "Terrain" && !alive) {
			anim.SetBool ("Landed", true);
		}
		else if (col.gameObject.tag == "Player") {
			rb.velocity = new Vector2 (0, 0);
			anim.SetBool ("Attack", true);
			attack = true;
		}

	}

	public void Remove () {
		Destroy(gameObject);
	}

	public void Angriff () {
		anim.SetBool ("Attack", false);
		attack = false;
	}

	public void Charge () {

		if ((target.position.x - transform.position.x) < 0) {
			rb.AddForce(new Vector2 ((-1) * chargeSpeed, 0));
		}
		else if ((target.position.x - transform.position.x) > 0) {
			rb.AddForce(new Vector2 ((1) * chargeSpeed, 0));
		}
		anim.SetBool ("Charge", false);
	}

	public void Charged () {
		Debug.Log("Reset1");
		canCharge = true;
		rb.velocity = new Vector2 (0, 0);
		anim.SetTrigger ("Charged");
	}

}
