using UnityEngine;
using System.Collections;

public class OrbFlügel : MonoBehaviour {

	public GameObject flügelPrefab;
	public Transform start;
	public LayerMask toHit;
	public float speed;
	public int projektilMax = 1;
	public int projektilCount;
	bool projektilShootable;

	// Use this for initialization
	void Start () {
	
	}
	
	void Update () {

		PlayerController pc = GetComponent<PlayerController>();

		if (projektilCount < projektilMax) {
			projektilShootable = true;
		}
		else {
			projektilShootable = false;
		}

		if (pc.shootWings && projektilShootable) {
			//AudioSource.PlayClipAtPoint (bombThrow, bombStart.position, throwVolume);
			//anim.SetTrigger ("Throw");
			Shoot();
		}

	}

	void Shoot () {

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

		projektilCount++;		//Counter für Projektile
		pc.shootWings = false;
	}
}
