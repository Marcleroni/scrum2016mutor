using UnityEngine;
using System.Collections;

public class EnemyFlying : MonoBehaviour {

	Rigidbody2D rb;
	public Transform target;
	public int Leben = 1;
	public float speed = 1;
	public bool facingRight = true;
	public float move;
	public bool alive = true;
	bool attack = false;
	Animator anim;
	public float distance;
	public float followDistance;
	public float chargeDistance;
	public bool canCharge = true;
	public float chargeSpeed = 150;
	Vector2 chargeDirection;
	public bool chargePuffer = false;

	public GameObject gameManager;
	public GameObject Player;

	public float rotationValue = 146;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		gameManager = GameObject.FindGameObjectWithTag("GameManager");
	}
		
	void FixedUpdate () {


		distance = Vector2.Distance(transform.position, target.position);

		if (!chargePuffer && alive && !attack && (distance < followDistance) && (distance > chargeDistance)) {

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
		else if (!chargePuffer && alive && !attack && (distance < followDistance) && (distance < chargeDistance) && canCharge) {

			chargePuffer = true;
			canCharge = false;
			anim.SetBool ("Charge", true);
			move = transform.position.x - target.position.x;

			if (move > 0 && !facingRight)									//Flip um Y-Achse
				Flip ();
			else if (move < 0 && facingRight)
				Flip ();
		}

		if ((Leben < 1) && alive) {
			Death ();
		}

	}

	public void Flip () {

		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}

	void OnCollisionEnter2D(Collision2D col) {

		if (((col.gameObject.tag == "Terrain") || (col.gameObject.tag == "LaserDestruction")) && chargePuffer) {
			rb.velocity = new Vector2 (0, 0);
		}
		else if (col.gameObject.tag == "Player" && alive) {

			rb.isKinematic = true;
			GameManager manager = gameManager.GetComponent<GameManager>();
			manager.Leben--; 
			rb.velocity = new Vector2 (0, 0);
			anim.SetBool ("Attack", true);
			attack = true;
		}

	}

	void OnCollisionExit2D(Collision2D col) {

		if (col.gameObject.tag == "Player" && alive) {
			rb.isKinematic = false;
			rb.velocity = new Vector2 (0, 0);
		}
	}

	void OnCollisionStay2D(Collision2D col) {

		if (col.gameObject.tag == "Player" && alive) {
			anim.SetBool ("Attack", true);
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Terrain" && !alive) {
			rb.isKinematic = true;
			anim.SetBool ("Landed", true);
		}
	}

	public void Death () {

		rb.isKinematic = false;
		alive = false;
		rb.velocity = new Vector2 (0, 0);
		if (!facingRight)									
			transform.Rotate (0,-180,rotationValue);
		else if (facingRight)
			transform.Rotate (0,-180,-rotationValue);
		gameObject.GetComponent<PolygonCollider2D> ().isTrigger = true;
	}


	public void Remove () {
		Destroy(gameObject);
	}

	public void Angriff () {
		anim.SetBool ("Attack", false);
		attack = false;
		chargePuffer = false;
		canCharge = true;
	}

	public void Charge () {

		chargeDirection = target.transform.position - transform.position;

		rb.AddRelativeForce((chargeDirection.normalized) * chargeSpeed);

		move = transform.position.x - target.position.x;

		if (move > 0 && !facingRight)									//Flip um Y-Achse
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();

		anim.SetBool ("Charge", false);
	}

	public void Charged () {
		canCharge = true;
		rb.velocity = new Vector2 (0, 0);
		anim.SetTrigger ("Charged");
		anim.SetBool ("Charge", false);
		chargePuffer = false;
	}

	public void stop () {
		rb.velocity = new Vector2 (0, 0);
	}


}
