using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour {

	public Transform BombCheck;
	float BombRadius = 0.15f;
	public LayerMask whatIsExplodable;
	public AudioClip clip;
	bool rotate;

	// Use this for initialization
	void Start () {
		rotate = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (rotate) {
			transform.Rotate(0, 0, 200*Time.deltaTime);
		}
		else {
			transform.Rotate(0, 0, 0);
		}
	}

	void KillOnAnimationEnd() {

		gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
		AudioSource.PlayClipAtPoint (clip, BombCheck.position);

		Collider2D[] hitTarget = Physics2D.OverlapCircleAll(BombCheck.position,BombRadius,whatIsExplodable);

		if(hitTarget.Length > 0)
		{
			foreach(Collider2D go in hitTarget)
			{
				if (go.tag == "Stone") {
				//	if (go.GetComponent<StoneScript>().erzAttached == true) { //Check if Ore Attached and Instantiate on Explosion

				//		Instantiate(go.GetComponentInChildren<ErzScript>().erzDrop, go.transform.position, go.transform.rotation);
				//	}
				}

				Destroy(go.gameObject);
			}
		}

		rotate = false;
		gameObject.GetComponent<CircleCollider2D>().enabled = false;

		GameObject Player = GameObject.FindWithTag ("Player");
		LayBomb lbScript = Player.GetComponent<LayBomb>();
		lbScript.bombCount = lbScript.bombCount-1;
	}

	void delete() {

		Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D col) {

		rotate = false;
		if (col.gameObject.tag != "Bomb") {
		gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
		}
	}

}
