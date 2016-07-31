using UnityEngine;
using System.Collections;

public class Boss3 : MonoBehaviour {


	public GameObject gameManager;
	Animator anim;
	Rigidbody2D rb;
	public float speed;
	public float move = -1;
	public bool facingRight = false;

	public float xDist;
	public float yDist;
	public float dist;
	public float chaseVector;

	public Transform target;

	public bool inRange = false;
	public bool attackRange = false;

	public int BossLeben = 5;
	public bool alive = true;
	public bool gotHit = false;

	public GameObject splitter;

	public GameObject Ui;
	public bool uiActive = false;
	public bool uiTriggered = false;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag("GameManager");
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();

		GameManager manager = gameManager.GetComponent<GameManager>();
		if (manager.Bosse.Contains(gameObject.tag)) {
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {

		xDist = Mathf.Abs (target.position.x - transform.position.x);	//3
		yDist = Mathf.Abs (target.position.y - transform.position.y);	//0,6

		dist = (target.position.x - transform.position.x);

		if (dist > 0) {
			chaseVector = 1;
		} else if (dist < 0) {
			chaseVector = -1;
		}

		if (xDist > 0.62) {
			attackRange = false;
		} else if (xDist < 0.62) {
			attackRange = true;
		}


		if ((xDist < 3) && (yDist < 0.6)) {
			inRange = true;
		} else {
			inRange = false;
		}




		if (!inRange && (anim.GetBool("attack") == false) && alive) {
			rb.velocity = new Vector2 (move * speed, rb.velocity.y);

			if (move > 0 && !facingRight)
				Flip ();
			else if (move < 0 && facingRight)
				Flip ();

			if (facingRight) {
				move = 1;
			} else if (!facingRight) {
				move = -1;
			}

		} else if (inRange && !attackRange && (anim.GetBool("attack") == false) && alive) {
			rb.velocity = new Vector2 (chaseVector * speed, rb.velocity.y);

			if (chaseVector > 0 && !facingRight)
				Flip ();
			else if (chaseVector < 0 && facingRight)
				Flip ();
			
			if (facingRight) {
				move = 1;
			} else if (!facingRight) {
				move = -1;
			}

		} else if (inRange && attackRange && alive) {
			anim.SetBool ("attack", true);
			rb.velocity = new Vector2 (0, rb.velocity.y);
		}


		if (BossLeben < 1) {
			alive = false;
			anim.SetBool ("Death", true);
		}

		if (uiActive) {

			if (Input.GetKeyDown (KeyCode.O)) {
				Ui.SetActive (false);
				Time.timeScale = 1;
			}
		}
	}

	public void Die () {
		GameObject reward = (GameObject)Instantiate (splitter, transform.position, transform.rotation);
		GameManager manager = gameManager.GetComponent<GameManager> ();
		manager.Bosse.Add (gameObject.tag);
		manager.Leben = 5;
		manager.lebenTotal++;
		Destroy (gameObject);
	}

	public void ResetAttack () {
	
		anim.SetBool ("attack", false);
	}

	public void HitStartCheck () {

		gotHit = false;

		if (chaseVector > 0 && !facingRight)
			Flip ();
		else if (chaseVector < 0 && facingRight)
			Flip ();
	}

	public void Flip () {

		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}

	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.tag == "Terrain") {
			move = -move;
		} 

		if (coll.gameObject.tag == "Player") {
			GameManager manager = gameManager.GetComponent<GameManager> ();
			manager.Leben--;
		}

		if ((coll.gameObject.tag == "Projektil") && !uiTriggered) {
			uiActive = true;
			Ui.SetActive (true);
			Time.timeScale = 0;
			uiTriggered = true;
		}
	}

	void OnTriggerEnter2D (Collider2D col) {

		if (col.gameObject.tag == "Player" && !gotHit) {
			GameManager manager = gameManager.GetComponent<GameManager> ();
			manager.Leben--;
			gotHit = true;
		} 
	}

	void OnTriggerStay2D (Collider2D col) {

		if (col.gameObject.tag == "Player" && !gotHit) {
			GameManager manager = gameManager.GetComponent<GameManager> ();
			manager.Leben--;
			gotHit = true;
		} 
	}
}
