using UnityEngine;
using System.Collections;

public class BossProjektil : MonoBehaviour {

	public GameObject gameManager;
	public Transform player;
	public float speed = 1;
	public float xDirection;
	public float yDirection;
	public Vector2 dir;
	Rigidbody2D rb;
	public float duration = 5f;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag("GameManager");
		rb = GetComponent<Rigidbody2D> ();
		player = GameObject.FindGameObjectWithTag("PlayerHit").GetComponent<Transform>();
		xDirection = player.position.x-transform.position.x;
		yDirection = player.position.y-transform.position.y;
		dir = player.position;
		rb.AddForce (new Vector2(xDirection*speed,yDirection*speed));
	}

	// Update is called once per frame
	void Update () {
		duration -= Time.deltaTime;

		if (duration < 0) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D col) {

		if (col.gameObject.tag == "Player") {
			GameManager manager = gameManager.GetComponent<GameManager>();
			manager.Leben--;
			Destroy (gameObject);
		}
	}
}
