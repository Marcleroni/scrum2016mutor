using UnityEngine;
using System.Collections;

public class Boss1 : MonoBehaviour {

	Animator anim;
	Rigidbody2D rb;
	public float speed = 1;
	public bool facingRight = true;
	public float move;

	public int BossLeben = 5;
	bool alive = true;

	public Transform target;				//Angriffsziel = Player
	public float followDist = 4f;
	public float distance;					//Distanz zwischen Boss und Player
	public float chargeDistanceMax;			//Höchstabstand für Charge
	public float chargeDistanceMin;			//Mindestabstand für Charge
	public float attackDistance;			//ab welcher Distanz angegriffen wird im Nahkampf
	public float chargeDirection;
	public float chargeSpeed;				//Sprungweite
	public float chargeY;					//Sprunghöhe
	public Vector2 posAtChargeStart;		//Position des Spielers zu Charge-Beginn
	public bool isCharging = false;
	public float chargeLanding;
	public float landingDistance;
	public float yDist;
	public float yDistCharge;				//Checkt ab welchem Höhenunterschied der Boss springt

	public bool chargePuffer = false;
	public bool attackPuffer = false;
	public bool canHit = false;					//Zeitraumparameter in dem der der Schlag des Gegners gezählt wird

	public float SetKnockbackTime = 0.5f;		//So lang ist die Knockbackdauer
	float KnockbackTime = 1f;
	public bool KnockBackReset = false;			
	public bool CanGetKnockedBack = false;		//Zeitspanne vom Charge/in der geKnocked werden kann, evtl Events in Animation für Ende Anpassen

	public GameObject gameManager;
	public GameObject Player;

	public GameObject splitter;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		gameManager = GameObject.FindGameObjectWithTag("GameManager");
		KnockbackTime = SetKnockbackTime;		//Knockback aus Inspektor laden

		GameManager manager = gameManager.GetComponent<GameManager>();
		if (manager.Bosse.Contains(gameObject.tag)) {
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {

		yDist = Mathf.Abs (transform.position.y - target.position.y);

		distance = Vector2.Distance(transform.position, target.position);
		landingDistance = Vector2.Distance (transform.position, posAtChargeStart);

//--------------------------------------------------Movement Boss-----------------------------------------------------------------------
		if (((distance < chargeDistanceMin) || (distance > chargeDistanceMax) || (yDist > yDistCharge)) && !isCharging && (distance > attackDistance) && !chargePuffer && !attackPuffer && alive && (distance < followDist)) {
			transform.position = Vector2.MoveTowards (transform.position, (new Vector2 (target.position.x, transform.position.y)), speed * Time.deltaTime);

			move = transform.position.x - target.position.x;

			if (move > 0 && !facingRight)									//Flip um Y-Achse
			Flip ();
			else if (move < 0 && facingRight)
				Flip ();

			anim.SetFloat ("Speed", Mathf.Abs (move));
		} else if (((distance > chargeDistanceMin) && (distance < chargeDistanceMax)) && !isCharging && (yDist < yDistCharge) && !chargePuffer) {
			isCharging = true;
			anim.SetBool ("Charge", true);
		} else if ((landingDistance < chargeLanding) && isCharging) {
			rb.velocity = new Vector2 (0, 0);
			anim.SetBool ("Charge", false);
			isCharging = false;
			chargePuffer = true;
		}
//--------------------------------------------------Angriffs Sequenz einleiten-----------------------------------------------------------
		if (distance < attackDistance) {			
			anim.SetBool ("Charge", false);
			anim.SetBool ("Attack", true);
			attackPuffer = true;
		} else if (distance > attackDistance) {
			anim.SetBool ("Attack", false);
		}
//--------------------------------------------------Reset nach Knocknack-----------------------------------------------------------
		if (KnockBackReset) {
			KnockbackTime -= Time.deltaTime;
			if (KnockbackTime < 0) {
				PlayerController control = Player.GetComponent<PlayerController>();
				control.enabled = true;
				KnockbackTime = SetKnockbackTime;
				KnockBackReset = false;
			}
		}

//--------------------------------------------------Trigger für SterbeSequenz--------------------------------------------------------------
		if (BossLeben < 1) {
			alive = false;
			anim.SetBool ("Death", true);
		}

	}
//--------------------------------------------------Funktion für Richtungsänderung-----------------------------------------------------------
	public void Flip () {

		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}

//--------------------------------------------------Charge Attack-----------------------------------------------------------
	public void Charge () {

		CanGetKnockedBack = true;	//Aktivieren des Knockback Zeitraums

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

//--------------------------------------------------Reset nach Charge-----------------------------------------------------------
	public void AttackReset () {				
		rb.velocity = new Vector2 (0, 0);
		anim.SetBool ("Charge", false);
		isCharging = false;
		chargePuffer = false;
		attackPuffer = false;
	}
//--------------------------------------------------Reset nach Krallen-Hit------------------------------------------------------
	public void nearAttackReset () {
		attackPuffer = false;
		anim.SetBool ("Attack", false);
		chargePuffer = false;
	}
//--------------------------------------------------Zeitraum in dem Collision mit Player gecheckt wird (in Animation ändern)----
	public void nearAttackCheck () {
		canHit = true;
	}

	public void nearAttackCheckReset () {
		canHit = false;
	}
//--------------------------------------------------Knockback Zeitraum deaktivieren-----------------------------------------------------------
	public void KnockBackInactive () {
		CanGetKnockedBack = false;
	}
//--------------------------------------------------Sterbe Sequenz-----------------------------------------------------------
	public void Death () {
		GameObject reward = (GameObject)Instantiate (splitter, transform.position, transform.rotation);
		GameManager manager = gameManager.GetComponent<GameManager> ();
		manager.Bosse.Add (gameObject.tag);
		manager.Leben = 5;
		manager.lebenTotal++;
		Destroy (gameObject);
	}

//--------------------------------------------------CollisionDetection---------------------------------------------------------------
	void OnCollisionEnter2D(Collision2D col) {

		rb.velocity = new Vector2 (0, 0);

		if (col.gameObject.tag == "Player") {

			rb.isKinematic = true;						//Kraftübertragung auf Boss disabled

			if ((chargeDirection < 0) && CanGetKnockedBack) {										//Check ob Spieler beim Charge getroffen wurde (Charge nach links)
				PlayerController control = Player.GetComponent<PlayerController>();
				control.enabled = false;
				col.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (-7, 3);			//Knockbackkurve nach links
				KnockBackReset = true;

				GameManager manager = gameManager.GetComponent<GameManager>();						//Leben abziehen bei Charge
				manager.Leben--;

			} else if ((chargeDirection > 0) && CanGetKnockedBack) {								//Check ob Spieler beim Charge getroffen wurde (Charge nach rechts)
				PlayerController control = Player.GetComponent<PlayerController>();
				control.enabled = false;
				col.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (7, 3);			//Knockbackkurve nach rechts
				KnockBackReset = true;

				GameManager manager = gameManager.GetComponent<GameManager>();						//Leben abziehen bei Charge
				manager.Leben--;
			}
				
			anim.SetBool ("Charge", false);
			isCharging = false;
		}

	}

	void OnCollisionExit2D(Collision2D col) {

		if (col.gameObject.tag == "Player") {

			rb.isKinematic = false;		//Kraftübertragung auf Boss enabled/reset des Movements
		}
	}

}
