using UnityEngine;
using System.Collections;

public class OrbKopfItem : MonoBehaviour {

	public GameObject gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Player") {
			GameManager manager = gameManager.GetComponent<GameManager>();

			if (gameObject.GetComponent<Collider2D> ().tag == "KopfOrb") {
				manager.GotOrbKopf = true;
				manager.OrbCounter = 1;
				Destroy (gameObject);
			} else if (gameObject.GetComponent<Collider2D> ().tag == "BeinOrb") {
				manager.GotOrbBeine = true;
				manager.OrbCounter = 2;
				Destroy (gameObject);
			} else if (gameObject.GetComponent<Collider2D> ().tag == "KrallenOrb") {
				manager.GotOrbKrallen = true;
				manager.OrbCounter = 3;
				Destroy (gameObject);
			} else if (gameObject.GetComponent<Collider2D> ().tag == "FluegelOrb") {
				manager.GotOrbFlügel = true;
				manager.OrbCounter = 4;
				Destroy (gameObject);
			}
		}
	}

}
