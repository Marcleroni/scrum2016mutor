using UnityEngine;
using System.Collections;

public class OrbFlügel : MonoBehaviour {

	public GameObject flügelPrefab;
	public Transform start;
	public LayerMask toHit;
	public float speed;
	public bool projektilShootable;

	public float wingsCd = 5;
	public float wingsCount;
	public bool startCount = false;

	AudioSource audio;

	public AudioClip wings;
	public float wingsVolume;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		wingsCount = wingsCd;
	}
	
	void Update () {

		PlayerController pc = GetComponent<PlayerController>();

		if (wingsCount == wingsCd) {
			projektilShootable = true;
		}
		else {
			projektilShootable = false;
		}

		if (pc.shootWings && projektilShootable) {
			//AudioSource.PlayClipAtPoint (bombThrow, bombStart.position, throwVolume);
			//anim.SetTrigger ("Throw");
			Shoot();
			startCount = true;
		}

		if (startCount) {
			wingsCount -= Time.deltaTime;
		}

		if (wingsCount < 0) {
			startCount = false;
			wingsCount = wingsCd;
		}


	}

	void Shoot () {

		audio.PlayOneShot(wings,wingsVolume);

		PlayerController pc = gameObject.GetComponent<PlayerController>();

		GameObject windSlash = (GameObject)Instantiate(flügelPrefab, start.position, start.rotation);
		Rigidbody2D rbWind = windSlash.GetComponent<Rigidbody2D>();

		if (pc.facingRight) {
			rbWind.velocity = new Vector2(speed, 0);
		}
		else if (!pc.facingRight) {
			rbWind.velocity = new Vector2(-speed, 0);
			Vector3 theScale = rbWind.transform.localScale;
			theScale.x *= -1;
			rbWind.transform.localScale = theScale;
		}

		Physics2D.IgnoreCollision(rbWind.GetComponent<Collider2D>(),  GetComponent<Collider2D>());

		pc.shootWings = false;
	}
}
