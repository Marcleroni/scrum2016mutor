using UnityEngine;
using System.Collections;

public class LayBomb : MonoBehaviour {

	public GameObject bombPrefab;
	public Transform bombStart;
	public LayerMask ToHit;
	public int bombSpeed = 10;
	public int bombMax = 1;	//Bombs at a Time
	public int bombCount;
	bool bombShootable;
	Animator anim;
	public AudioClip bombThrow;
	public float throwVolume;
	public AudioClip bombDrop;
	public float dropVolume;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		if (bombCount < bombMax) {
			bombShootable = true;
		}
		else {
			bombShootable = false;
		}

		if (Input.GetButtonDown ("Fire1") && bombShootable) {

			AudioSource.PlayClipAtPoint (bombThrow, bombStart.position, throwVolume);
			anim.SetTrigger ("Throw");
			Shoot();
		}



		if (Input.GetButtonDown ("Fire2") && bombShootable) {

			AudioSource.PlayClipAtPoint (bombDrop, bombStart.position, dropVolume);
			anim.SetTrigger ("Throw");
			GameObject Bomb = Instantiate(bombPrefab, bombStart.position, transform.rotation) as GameObject;
			Bomb.transform.Rotate (0,0,90); //Dynamite horizontal
			bombCount++;
		}

	}

	void Shoot () {



		Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2 (bombStart.position.x, bombStart.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, mousePosition-firePointPosition, 100, ToHit);

		//RayCast für Dynamite

		RobotController rc = gameObject.GetComponent<RobotController>();

		if ((mousePosition.x < firePointPosition.x) && rc.facingRight) {
			rc.Flip ();
		}
		else if ((mousePosition.x > firePointPosition.x) && !rc.facingRight) {
			rc.Flip ();
		}

		//Character "Flippen" beim werfen

		GameObject bomb = (GameObject)Instantiate(bombPrefab, bombStart.position, bombStart.rotation);
		Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();
		Vector2 direction = mousePosition-firePointPosition;
		direction.Normalize();
		rb.velocity = (direction) * bombSpeed;

		bombCount++;

		//Debug.DrawLine (firePointPosition, (mousePosition-firePointPosition)*100);
		if (hit.collider != null) {
			Debug.DrawLine (firePointPosition, hit.point);
		}
	}


}
