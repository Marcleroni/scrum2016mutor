using UnityEngine;
using System.Collections;

public class RobotController : MonoBehaviour {

	public float maxSpeed = 1.6f;
	public bool facingRight = true;
	Rigidbody2D rb;
	Animator anim;

	bool grounded = false;
	bool grounddist = false;
	public Transform groundCheck;
	public Transform distCheck;
	float groundRadius = 0.10f;
	public LayerMask whatIsGround;
	public float jumpForce = 200;

	public AudioClip land;						//Sound Jump/Land
	public float landVolume;
	public AudioClip jump;
	public float jumpVolume;

	bool doubleJump = true;						//doubleJump Variable
	public bool doubleJumpEnabled = false;		//doubleJump ON/OFF

	void Start () {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}
	

	void FixedUpdate () {

		anim = GetComponent<Animator>();

		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);

		if (grounded && doubleJumpEnabled)				//doubleJump
			doubleJump = false;

		anim.SetFloat ("vSpeed", rb.velocity.y);


		float move = Input.GetAxis ("Horizontal");

		anim.SetFloat ("Speed", Mathf.Abs (move));

		rb.velocity = new Vector2(move * maxSpeed, rb.velocity.y);

		if (move > 0 && !facingRight)
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();
	}

	void Update () {
						 //(Zeile drunter) Jumping Puffer für Schrägen
		if(((grounded && (rb.velocity.y > -0.2 && rb.velocity.y < 0.2))|| !doubleJump) && Input.GetButtonDown ("Jump")) {	//doubleJump or Parameter

			AudioSource.PlayClipAtPoint(jump,this.GetComponent<Transform>().position, jumpVolume);

			anim.SetBool ("Ground", false);
			rb.AddForce(new Vector2 (0, jumpForce));

			if (!doubleJump && !grounded)	//doubleJump
				doubleJump = true;			//doubleJump
		}


		if (rb.velocity.y > 0) {
			anim.SetBool ("Fall", false);
		}
		else if (rb.velocity.y < 0) {
			anim.SetBool ("Fall", true);
		}
		else {
			anim.SetBool ("Fall", false);
		}

		grounddist = Physics2D.Linecast(groundCheck.position, distCheck.position, whatIsGround); //Smooth landing animation
		anim.SetBool ("Dist", grounddist);

	}

	public void Flip () {
		
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		
	}

	void Landing () {

		//Debug.Log ("Landing");
		AudioSource.PlayClipAtPoint(land,this.GetComponent<Transform>().position, landVolume);
	}

}