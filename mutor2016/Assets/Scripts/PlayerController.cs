using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float maxSpeed = 1.6f;
	public bool facingRight = true;
	Rigidbody2D rb;
	Animator anim;

	bool grounded = false;
	bool grounddist = false;
	public Transform groundCheck;
	public Transform distCheck;
	float groundRadius = 0.02f;
	public LayerMask whatIsGround;
	public float jumpForce = 200;

	bool doubleJump = true;						//doubleJump Variable
	public bool doubleJumpEnabled = false;		//doubleJump ON/OFF

	public AudioClip jump;
	public float jumpVolume;
	AudioSource audio;

	public bool wallJumpEnabled = false;		//wallJump ON/OFF
	public bool wallSlideEnabled = false;
	public LayerMask whatIsWall;
	public Transform wallCheck;
	float wallRadius = 0.1f;
	public bool onWall = false;

	public float wallJumpForce = 200f;
	public float wallJumpPushForce = 500f;

	public float wallJumpControlDelay = 0.15f;
	public float wallJumpDelayCalc = 0;

	public GameObject gameManager;


	void Start () {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		audio = GetComponent<AudioSource>();
		gameManager = GameObject.FindGameObjectWithTag("GameManager");

		}

	void FixedUpdate () {

		float move = Input.GetAxis ("Horizontal");						//normales Movement
		rb.velocity = new Vector2(move * maxSpeed, rb.velocity.y); 

		if (move > 0 && !facingRight)									//Flip um Y-Achse
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();

		anim.SetFloat ("Speed", Mathf.Abs (move));						//Walk transition

		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);	//Ground detection
		anim.SetBool ("Ground", grounded);														//Animator Variable

		if (grounded && doubleJumpEnabled)												//doubleJump reset
			doubleJump = false;

		GameManager manager = gameManager.GetComponent<GameManager>();

		if (manager.OrbKopf == true) {
			anim.SetBool("OrbKopf",true);

		}

	}

	void Update () {

		if ((grounded || (!doubleJump)) && Input.GetButtonDown ("Jump")) {	//doubleJump or Parameter

			audio.time = 0.5f;
			audio.Play();
			//AudioSource.PlayClipAtPoint(jump,this.GetComponent<Transform>().position, jumpVolume);

			anim.SetBool ("Ground", false);
			rb.velocity = new Vector2 (rb.velocity.x, 0);
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



		onWall = Physics2D.OverlapCircle(wallCheck.position, wallRadius, whatIsWall);	//Wall detection

		if (onWall && !grounded && Input.GetButtonDown ("Jump") && wallJumpEnabled) {					// WallJump
			wallJumpDelayCalc -= Time.deltaTime;
			rb.velocity = new Vector2 (0, 0);
			rb.gravityScale = 1f;
			rb.AddForce(new Vector2((wallJumpPushForce * (facingRight ? -1:1)), wallJumpForce));
		}
	
		if (grounded) {											//Reset Wall Jump Delay on Ground
			rb.gravityScale = 1f;
			wallJumpDelayCalc = wallJumpControlDelay;
		}
			
		if (wallJumpDelayCalc < 0) {
			rb.gravityScale = 1f;
			wallJumpDelayCalc = wallJumpControlDelay;
		}

	
	}


	void OnCollisionEnter2D(Collision2D coll) {
		if ((coll.gameObject.layer == 9) && wallSlideEnabled) {
			wallJumpDelayCalc = wallJumpControlDelay;
		}
	}

	void OnCollisionStay2D(Collision2D coll) {
		if ((coll.gameObject.layer == 9) && wallSlideEnabled) {

			if(facingRight && (wallJumpDelayCalc == wallJumpControlDelay) && (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)) {			//Wall Slide Right
				rb.velocity = new Vector2 (rb.velocity.x, 0);
				rb.gravityScale = 0.3f;
			}
			else if (!facingRight && (wallJumpDelayCalc == wallJumpControlDelay) && (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)) {	//Wall Slide Left
				rb.velocity = new Vector2 (rb.velocity.x, 0);
				rb.gravityScale = 0.3f;
			}
			else {
				rb.gravityScale = 1f;
			}
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		if ((coll.gameObject.layer == 9) && wallSlideEnabled) {
			rb.gravityScale = 1f;
			wallJumpDelayCalc -= Time.deltaTime;
		}

	}


	public void Flip () {

		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}
		
}
