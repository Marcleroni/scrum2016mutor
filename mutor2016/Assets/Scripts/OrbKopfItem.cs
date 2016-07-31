using UnityEngine;
using System.Collections;

public class OrbKopfItem : MonoBehaviour {

	public GameObject gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag("GameManager");
		GameManager manager = gameManager.GetComponent<GameManager>();
		if (manager.Items.Contains(gameObject.tag)) {
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Player") {
			GameManager manager = gameManager.GetComponent<GameManager>();
			manager.playOrb ();

			if (gameObject.tag == "KopfOrb") {
				manager.Items.Add (gameObject.tag);
				manager.GotOrbKopf = true;
				manager.OrbCounter = 1;
				Destroy (gameObject);
			} else if (gameObject.tag == "BeinOrb") {
				manager.Items.Add (gameObject.tag);
				manager.GotOrbBeine = true;
				manager.OrbCounter = 2;
				Destroy (gameObject);
			} else if (gameObject.tag == "KrallenOrb") {
				manager.Items.Add (gameObject.tag);
				manager.GotOrbKrallen = true;
				manager.OrbCounter = 3;
				Destroy (gameObject);
			} else if (gameObject.tag == "FluegelOrb") {
				manager.Items.Add (gameObject.tag);
				manager.GotOrbFlügel = true;
				manager.OrbCounter = 4;
				Destroy (gameObject);
			}
		}
	}

}
