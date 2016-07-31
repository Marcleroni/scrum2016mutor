using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	float maxSpeed;
	public float normalSpeed = 1.6f;

	public bool facingRight = true;
	Rigidbody2D rb;
	Animator anim;

	public float move;

	bool grounded = false;
	bool grounddist = false;
	bool dist1 = false;
	bool dist2 = false;
	public Transform groundCheck;
	public Transform distCheck;
	public Transform distCheck2;
	public Transform checkLanding1;
	public Transform checkLanding2;
	public float groundRadius = 0.02f;
	public LayerMask whatIsGround;
	public float jumpForce = 200;
	public float doublejumpForce = 200;

	bool doubleJump = true;						//doubleJump Variable
	public bool doubleJumpEnabled = false;		//doubleJump ON/OFF

	public AudioClip jump;
	public float jumpVolume;
	public AudioClip walk;
	public float walkVolume;
	public AudioClip death;
	public float deathVolume;
	public AudioClip claw;
	public float clawVolume;
	AudioSource audio;
	public AudioClip slide;
	public float slideVolume;

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

	public bool fastRunning = false;
	public float runningSpeed = 4.0f;

	public bool shootLaser = false;

	public bool shootWings = false;

	public bool shootBeine = false;

	public KeyCode ActionKey = KeyCode.O; //Taste für alle Attacken

	public GameObject gameManager;


	public Transform beinTriggerSpawn;
	public GameObject beinTrigger;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		audio = GetComponent<AudioSource>();
		gameManager = GameObject.FindGameObjectWithTag("GameManager");

		GameManager manager = gameManager.GetComponent<GameManager>();
		manager.Leben = 5;
		manager.alive = true;
		}

	void FixedUpdate () {

		GameManager manager = gameManager.GetComponent<GameManager>();

		if (manager.alive) {
			move = Input.GetAxis ("Horizontal");			//normales Movement

			rb.velocity = new Vector2(move * maxSpeed, rb.velocity.y); 

			if (move > 0 && !facingRight)									//Flip um Y-Achse
				Flip ();
			else if (move < 0 && facingRight)
				Flip ();

			anim.SetFloat ("Speed", Mathf.Abs (move));						//Walk transition
		}
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);	//Ground detection
		anim.SetBool ("Ground", grounded);														//Animator Variable

		if (grounded && doubleJumpEnabled)												//doubleJump reset
			doubleJump = false;

//-------------------------------------------------- Orb-Controller --------------------------------------------------------------------



		if (manager.OrbKopf == true) {					//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Orb Animator Bool
			anim.SetBool ("OrbKopf", true);
			anim.SetBool ("OrbKrallen", false);
			anim.SetBool ("OrbBeine", false);
			anim.SetBool ("OrbFluegel", false);

			doubleJumpEnabled = false;
			doubleJump = true;

			wallJumpEnabled = false;
			wallSlideEnabled = false;

			fastRunning = false;

		} else if (manager.OrbKrallen == true) {
			anim.SetBool ("OrbKopf", false);
			anim.SetBool ("OrbKrallen", true);
			anim.SetBool ("OrbBeine", false);
			anim.SetBool ("OrbFluegel", false);

			doubleJumpEnabled = false;
			doubleJump = true;

			wallJumpEnabled = true;
			wallSlideEnabled = true;

			fastRunning = false;

		} else if (manager.OrbBeine == true) {
			anim.SetBool ("OrbKopf", false);
			anim.SetBool ("OrbKrallen", false);
			anim.SetBool ("OrbBeine", true);
			anim.SetBool ("OrbFluegel", false);

			doubleJumpEnabled = false;
			doubleJump = true;

			wallJumpEnabled = false;
			wallSlideEnabled = false;

			fastRunning = true;

		} else if (manager.OrbFlügel == true) {
			anim.SetBool ("OrbKopf", false);
			anim.SetBool ("OrbKrallen", false);
			anim.SetBool ("OrbBeine", false);
			anim.SetBool ("OrbFluegel", true);

			doubleJumpEnabled = true;

			wallJumpEnabled = false;
			wallSlideEnabled = false;

			fastRunning = false;
		}
	}

//-------------------------------------------------- Update --------------------------------------------------------------------
	void Update () {

		GameManager manager = gameManager.GetComponent<GameManager>();

		if ((grounded) && Input.GetButtonDown ("Jump") && manager.alive) {	//Jump

			audio.PlayOneShot(jump,jumpVolume);

			anim.SetBool ("Ground", false);
			rb.velocity = new Vector2 (rb.velocity.x, 0);
			rb.AddForce(new Vector2 (0, jumpForce));

		}

		if ((!doubleJump && !grounded) && Input.GetButtonDown ("Jump") && manager.alive) {	//doubleJump

			audio.PlayOneShot(jump,jumpVolume);

			anim.SetBool ("Ground", false);
			rb.velocity = new Vector2 (rb.velocity.x, 0);
			rb.AddForce(new Vector2 (0, doublejumpForce));

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


		dist1 = Physics2D.Linecast(checkLanding1.position, distCheck.position, whatIsGround); //Smooth landing animation
		dist2 = Physics2D.Linecast(checkLanding2.position, distCheck2.position, whatIsGround);

		if (dist1 || dist2) {
			grounddist = true;
		} else {
			grounddist = false;
		}
		anim.SetBool ("Dist", grounddist);



		onWall = Physics2D.OverlapCircle(wallCheck.position, wallRadius, whatIsWall);	//Wall detection

		if (onWall && !grounded && Input.GetButtonDown ("Jump") && wallJumpEnabled && manager.alive) {					// WallJump
			wallJumpDelayCalc -= Time.deltaTime;
			rb.velocity = new Vector2 (0, 0);
			rb.gravityScale = 1f;
			rb.AddForce(new Vector2((wallJumpPushForce * (facingRight ? -1:1)), wallJumpForce));
		}
	
		if (grounded) {											//Reset Wall Jump Delay on Ground
			rb.gravityScale = 1f;
			wallJumpDelayCalc = wallJumpControlDelay;
			anim.SetBool ("Climb", false);
		}
			
		if (wallJumpDelayCalc < 0) {
			rb.gravityScale = 1f;
			wallJumpDelayCalc = wallJumpControlDelay;
		}

		if (fastRunning) {
			maxSpeed = runningSpeed;
		} else {
			maxSpeed = normalSpeed;
		}

//-------------------------------------------------- Attack-Controller --------------------------------------------------------------------

		if (Input.GetKeyDown (ActionKey) && manager.alive) {

			if (manager.OrbKopf == true) {
				if (GetComponent<OrbKopf> ().laserShootable == true) {
					shootLaser = true;
				}
			} else if (manager.OrbKrallen == true) {
				anim.SetBool ("AttackKrallen", true);
				audio.PlayOneShot(claw,clawVolume);
			} else if (manager.OrbFlügel == true) {
				if (GetComponent<OrbFlügel> ().projektilShootable == true) {
					shootWings = true;
					anim.SetBool ("AttackWings", true);
				}
			} else if ((manager.OrbBeine == true) && (anim.GetBool("Ground") == false) && (anim.GetBool("Fall") == false) && !shootBeine){
				shootBeine = true;
				//anim.SetBool ("BeinAttacke", true);
				GameObject beinIndikator = (GameObject)Instantiate (beinTrigger, beinTriggerSpawn.position, beinTriggerSpawn.rotation);
				beinIndikator.transform.parent = gameObject.transform;
			}
		}
	
	}
//-------------------------------------------------- Krallen Reset ----------------------------------------------------------------------

	public void KrallenReset () {
		anim.SetBool ("AttackKrallen", false);
	}

//-------------------------------------------------- Wings Reset ----------------------------------------------------------------------

	public void WingsReset () {
		anim.SetBool ("AttackWings", false);
	}

//-------------------------------------------------- Death ----------------------------------------------------------------------

	public void Death () {
		anim.SetBool ("Death", true);
		anim.SetTrigger ("DeathTrigger");
		rb.constraints = RigidbodyConstraints2D.FreezeAll;
		audio.PlayOneShot(death,deathVolume);
	}

//-------------------------------------------------- Death ----------------------------------------------------------------------

	public void Respawn () {

		GameManager manager = gameManager.GetComponent<GameManager>();
		manager.lebenTotal--;
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
	}
//-------------------------------------------------- Collision Enter --------------------------------------------------------------------
	void OnCollisionEnter2D(Collision2D coll) {

		if ((coll.gameObject.layer == 9) && wallSlideEnabled) {
			wallJumpDelayCalc = wallJumpControlDelay;
			anim.SetBool ("Climb", true);
			Debug.Log ("TestEnter");
		}
			
	}

//-------------------------------------------------- Collision Stay --------------------------------------------------------------------

	void OnCollisionStay2D(Collision2D coll) {
		if ((coll.gameObject.layer == 9) && wallSlideEnabled) {
			anim.SetBool ("Climb", true);
			Debug.Log ("TestStay");
			if(facingRight && (wallJumpDelayCalc == wallJumpControlDelay) && (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") > 0) && onWall) {			//Wall Slide Right
				rb.velocity = new Vector2 (rb.velocity.x, 0);
				rb.gravityScale = 0.3f;
				Debug.Log ("TestSlide");
				audio.PlayOneShot(slide,slideVolume);
			}
			else if (!facingRight && (wallJumpDelayCalc == wallJumpControlDelay) && (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") < 0) && onWall) {	//Wall Slide Left
				rb.velocity = new Vector2 (rb.velocity.x, 0);
				rb.gravityScale = 0.3f;
				Debug.Log ("TestSlide");
				audio.PlayOneShot(slide,slideVolume);
			}
			else {
				rb.gravityScale = 1f;
			}
		}
	}

//-------------------------------------------------- Collision Exit --------------------------------------------------------------------

	void OnCollisionExit2D(Collision2D coll) {
		if ((coll.gameObject.layer == 9) && wallSlideEnabled) {
			rb.gravityScale = 1f;
			//wallJumpDelayCalc = 0;//Time.deltaTime;
			anim.SetBool ("Climb", false);
		}

	}


	public void Flip () {

		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}

	public void walkSound () {
		audio.PlayOneShot(walk,walkVolume);
	}

	public void beineAttack () {
		//audio.PlayOneShot(beine,beineVolume);
	}
		
}
