using UnityEngine;
using System.Collections;

public class Boss1 : MonoBehaviour {

	Animator anim;
	Rigidbody2D rb;
	public float speed = 1;
	public bool facingRight = true;
	public float move;

	public Transform target;
	public float distance;
	public float chargeDistanceMax;
	public float chargeDistanceMin;
	public float attackDistance;
	public float chargeDirection;
	public float chargeSpeed;
	public float chargeY;
	public Vector2 posAtChargeStart;
	public bool isCharging = false;
	public float chargeLanding;
	public float landingDistance;
	public float yDist;
	public float yDistCharge;	//Checkt ab welchem Höhenunterschied der Boss springt

	public GameObject gameManager;
	public GameObject Player;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		gameManager = GameObject.FindGameObjectWithTag("GameManager");
	}
	
	// Update is called once per frame
	void Update () {

		yDist = Mathf.Abs (transform.position.y - target.position.y);

		distance = Vector2.Distance(transform.position, target.position);
		landingDistance = Vector2.Distance (transform.position, posAtChargeStart);

		if (((distance < chargeDistanceMin) || (distance > chargeDistanceMax) || (yDist > yDistCharge)) && !isCharging && (distance > attackDistance)) {
			transform.position = Vector2.MoveTowards (transform.position, (new Vector2 (target.position.x, transform.position.y)), speed * Time.deltaTime);

			move = transform.position.x - target.position.x;

			if (move > 0 && !facingRight)									//Flip um Y-Achse
			Flip ();
			else if (move < 0 && facingRight)
				Flip ();

			anim.SetFloat ("Speed", Mathf.Abs (move));
		} else if (((distance > chargeDistanceMin) && (distance < chargeDistanceMax)) && !isCharging && (yDist < yDistCharge)) {
			isCharging = true;
			anim.SetBool ("Charge", true);
		} else if ((landingDistance < chargeLanding) && isCharging) {
			rb.velocity = new Vector2 (0, 0);
			anim.SetBool ("Charge", false);
			isCharging = false;
		}

		if (distance < attackDistance) {
			//rb.velocity = new Vector2 (0, 0);
			anim.SetBool ("Charge", false);
			anim.SetBool ("Attack", true);
		} else if (distance > attackDistance) {
			anim.SetBool ("Attack", false);
		}


	}

	public void Flip () {

		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}

	public void Charge () {

		posAtChargeStart = target.position;

		chargeDirection = target.transform.position.x - transform.position.x;

		rb.velocity = new Vector2 (0, 0);

		if (chargeDirection < 0) {
			rb.AddRelativeForce (Vector2.left * chargeSpeed);
			rb.AddRelativeForce( new Vector2(0,chargeY));				//Sprunghöhe
		} else if (chargeDirection > 0) {
			rb.AddRelativeForce (Vector2.right * chargeSpeed);
			rb.AddRelativeForce( new Vector2(0,chargeY));				//Sprunghöhe
		}

	}

	public void AttackReset () {
		rb.velocity = new Vector2 (0, 0);
		anim.SetBool ("Charge", false);
		isCharging = false;
	}

	void OnCollisionEnter2D(Collision2D col) {

		if (col.gameObject.tag == "Player") {
			
			if ((chargeDirection < 0) && isCharging) {
				PlayerController control = Player.GetComponent<PlayerController>();
				Debug.Log ("TEst");
				//control.moveTrigger = true;
				//control.move = -1f;
				//control.enabled = false;
				//col.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (-10, 6);
				//col.gameObject.GetComponent<Rigidbody2D>().AddForce (new Vector2 (-2000, 300));
			} else if ((chargeDirection > 0) && isCharging) {
				//col.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce (new Vector2 (500, 300));
			}

			//Debug.Log ("Hit");
			anim.SetBool ("Charge", false);
			isCharging = false;
		}

	}

//-------------------------------------------------- Krallen Hit --------------------------------------------------------------------

	void OnTriggerStay2D (Collider2D col) {
		if (col.gameObject.tag == "Player") {
			GameManager manager = gameManager.GetComponent<GameManager>();
			PlayerController control = Player.GetComponent<PlayerController>();
			if (manager.OrbKrallen && Input.GetKeyDown (control.ActionKey)) {
				Debug.Log ("BossHit");
			}
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Player") {
			GameManager manager = gameManager.GetComponent<GameManager>();
			PlayerController control = Player.GetComponent<PlayerController>();
			if (manager.OrbKrallen && Input.GetKeyDown (control.ActionKey)) {
				Debug.Log ("BossHit");
			}
		}
	}

}
