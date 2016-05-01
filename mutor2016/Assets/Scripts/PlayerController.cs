using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float maxSpeed = 1.6f;
	public bool facingRight = true;
	Rigidbody2D rb;

	bool grounded = false;
	bool grounddist = false;
	public Transform groundCheck;
	public Transform distCheck;
	float groundRadius = 0.10f;
	public LayerMask whatIsGround;
	public float jumpForce = 200;

	bool doubleJump = true;						//doubleJump Variable
	public bool doubleJumpEnabled = false;		//doubleJump ON/OFF



	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate () {

		float move = Input.GetAxis ("Horizontal");						//normales Movement
		rb.velocity = new Vector2(move * maxSpeed, rb.velocity.y); 

		if (move > 0 && !facingRight)									//Flip um Y-Achse
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();

		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

		if (grounded && doubleJumpEnabled)								//doubleJump reset
			doubleJump = false;

	}



	void Update () {

		if((grounded || !doubleJump) && Input.GetButtonDown ("Jump")) {	//doubleJump or Parameter

			//AudioSource.PlayClipAtPoint(jump,this.GetComponent<Transform>().position, jumpVolume);

			//anim.SetBool ("Ground", false);
			rb.velocity = new Vector2 (rb.velocity.x, 0);
			rb.AddForce(new Vector2 (0, jumpForce));


			grounddist = Physics2D.Linecast(groundCheck.position, distCheck.position, whatIsGround); //Smooth landing animation
			//anim.SetBool ("Dist", grounddist);


			if (!doubleJump && !grounded)	//doubleJump
				doubleJump = true;			//doubleJump

		}


	}


	public void Flip () {

		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}

}
