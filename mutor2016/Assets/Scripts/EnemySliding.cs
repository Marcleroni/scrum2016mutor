using UnityEngine;
using System.Collections;

public class EnemySliding : MonoBehaviour {

	public GameObject gameManager;
	Animator anim;
	Rigidbody2D rb;
	public Transform target;
	public float distanceX;
	public float distanceY;
	public float slideDist = 5f;
	public float slidePufferY = 3f;
	public bool isSliding = false;
	public float speed = 5f;
	public int dir;
	public int dirCheck;
	public bool facingRight = true;
	float move;
	public bool canSlide = true;
	public int Leben = 1;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag("GameManager");
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		distanceX = Mathf.Abs(transform.position.x - target.position.x);
		distanceY = Mathf.Abs(transform.position.y - target.position.y);

		if (transform.position.x > target.position.x) {
			dir = -1;
		} else if (transform.position.x < target.position.x) {
			dir = 1;
		}

		if ((distanceX < slideDist) && (distanceY < slidePufferY) && !isSliding && canSlide) {
			dirCheck = dir;
			anim.SetBool ("Sliding", true);
		}

		if ((anim.GetBool ("Sliding") == true) && canSlide) {
			rb.isKinematic = false;
			isSliding = true;
			canSlide = false;
			rb.velocity = new Vector2 (dirCheck * speed, rb.velocity.y);
		}


		move = transform.position.x - target.position.x;
		if (move > 0 && !facingRight && !isSliding)								
			Flip ();
		else if (move < 0 && facingRight && !isSliding)
			Flip ();

		if (Leben < 1) {
			anim.SetBool ("Alive", false);
		}


	}

	public void Flip () {

		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}

	void OnTriggerEnter2D(Collider2D col) {
		/*
		if (((col.gameObject.tag == "Terrain") || (col.gameObject.tag == "LaserDestruction")) && isSliding) {
			anim.SetBool ("Sliding", false);
			rb.velocity = new Vector2 (0, 0);
			rb.AddForce(new Vector2(-dirCheck*20,150));
			isSliding = false;
		}
		*/
		if ((col.gameObject.tag == "Player") && isSliding) {
			GameManager manager = gameManager.GetComponent<GameManager>();
			manager.Leben--;

			anim.SetBool ("Sliding", false);
			rb.velocity = new Vector2 (0, 0);
			rb.AddForce(new Vector2(-dirCheck*20,150));
			isSliding = false;
		}
	}

	void OnCollisionStay2D(Collision2D col) {

		if ((col.gameObject.tag == "Player")) {
			rb.isKinematic = true;
		}
	}

	void OnCollisionExit2D(Collision2D col) {

		if ((col.gameObject.tag == "Player") && isSliding) {
			rb.isKinematic = false;
		}
	}


	public void speedReset () {
		rb.velocity = new Vector2 (0, 0);
		rb.isKinematic = true;
	}

	public void Reset () {
		canSlide = true;

		if (move > 0 && !facingRight && !isSliding)								
			Flip ();
		else if (move < 0 && facingRight && !isSliding)
			Flip ();

		rb.velocity = new Vector2 (0, 0);
	}

	public void Death() {
		Destroy (gameObject);
	}

}
