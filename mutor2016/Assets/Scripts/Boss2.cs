using UnityEngine;
using System.Collections;

public class Boss2 : MonoBehaviour {

	public GameObject gameManager;
	public Transform target;
	public Transform spawn;
	float move;
	public float xDist;
	public float yDist;
	public GameObject ball;
	public bool attack = false;
	public bool attackTrigger = true;
	Animator anim;
	public bool facingRight = true;

	public int BossLeben = 5;
	public bool alive = true;

	public GameObject splitter;

	AudioSource audio;

	public AudioClip wurf;
	public float wurfVolume;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		gameManager = GameObject.FindGameObjectWithTag("GameManager");
		audio = GetComponent<AudioSource> ();

		GameManager manager = gameManager.GetComponent<GameManager>();
		if (manager.Bosse.Contains(gameObject.tag)) {
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {

		xDist = Mathf.Abs (target.position.x - transform.position.x);	//4
		yDist = Mathf.Abs (target.position.y - transform.position.y);	//2,15
		move = transform.position.x - target.position.x;

		if (move > 0 && !facingRight)								
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();
		

		if ((xDist < 4) && (yDist < 2.14) && alive) {
			attack = true;
		} else {
			attack = false;
		}

		if (attack && attackTrigger) {
			anim.SetBool ("shoot", true);
		}

		if (BossLeben < 1) {
			alive = false;
			anim.SetBool ("Death", true);
		}

	}

	public void Reset () {
		attackTrigger = true;
	}

	public void startReset () {
		anim.SetBool ("shoot", false);
	}

	public void Shoot () {
		audio.PlayOneShot(wurf,wurfVolume);
		GameObject mumpel = (GameObject)Instantiate (ball, spawn.position, spawn.rotation);

	}

	public void Flip () {

		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}

	public void Death () {
		GameObject reward = (GameObject)Instantiate (splitter, spawn.position, spawn.rotation);
		GameManager manager = gameManager.GetComponent<GameManager> ();
		manager.Bosse.Add (gameObject.tag);
		manager.Leben = 5;
		manager.lebenTotal++;
		Destroy (gameObject);
	}
}
