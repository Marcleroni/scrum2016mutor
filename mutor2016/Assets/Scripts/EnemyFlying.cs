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

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	void FixedUpdate () {

		distance = Vector2.Distance(transform.position, target.position);

		if (alive && !attack && (distance < followDistance)) {
			
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
			if (!facingRight)									
				transform.Rotate (0,-180,146);
			else if (facingRight)
				transform.Rotate (0,-180,-146);
		}
		else if (col.gameObject.tag == "Terrain" && !alive) {
			anim.SetBool ("Landed", true);
		}
		else if (col.gameObject.tag == "Player") {
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

}
