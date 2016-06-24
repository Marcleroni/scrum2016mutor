using UnityEngine;
using System.Collections;

public class Splitter : MonoBehaviour {

	public GameObject gameManager;
	bool movement = true;
	public Vector2 move = Vector2.up;
	public float range = 2.0f;
	public float speed = 0.5f;

	GameObject Bouncing;
	Transform start;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag("GameManager");
		Bouncing = gameObject;
		start = gameObject.GetComponent<Transform> ();
	}

	// Update is called once per frame
	void Update () {

		if (movement) {
			Bouncing.transform.position = (Vector2)start.position + move * (range * Mathf.Sin (Time.timeSinceLevelLoad * speed));
		}

	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Player") {
			GameManager manager = gameManager.GetComponent<GameManager>();
			manager.SplitterCounter++;
			Destroy(gameObject);
		}
	}
}
