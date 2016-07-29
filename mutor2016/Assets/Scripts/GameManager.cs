using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour {

	public GameObject player;
	public static GameManager instance = null;

	public int Leben = 5;
	public bool alive = true;

	public int SplitterCounter = 0;

	public int OrbCounter = 0;

	public bool OrbKopf = false;
	public bool GotOrbKopf = false;

	public bool OrbBeine = false;
	public bool GotOrbBeine = false;

	public bool OrbKrallen = false;
	public bool GotOrbKrallen = false;

	public bool OrbFlügel = false;
	public bool GotOrbFlügel = false;

	public int fromLevel = 0;

	public List<string> Items = new List<string>();
		
	void Awake () {

		player = GameObject.Find("Player");

		if (instance == null) {
			instance = this;
		}
		else if (instance != this) {
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);

	}
	
	// Update is called once per frame
	void Update () {

		if ((Leben < 1) && alive) {
			PlayerController control = player.GetComponent<PlayerController> ();
			control.Death ();
			alive = false;
		}

		if (Input.GetKeyDown (KeyCode.O) && (OrbCounter > 0)) {
			OrbCounter++;
		}

//---------------------------- Orb-Controller ------------------------------

		if (OrbCounter == 1) {
			if (GotOrbKopf) {
				OrbKopf = true;
				OrbBeine = false;
				OrbKrallen = false;
				OrbFlügel = false;
			}
			else if (!GotOrbKopf) {
				OrbCounter++;
			}
		}
		else if (OrbCounter == 2) {
			if (GotOrbBeine) {
				OrbKopf = false;
				OrbBeine = true;
				OrbKrallen = false;
				OrbFlügel = false;
			}
			else if (!GotOrbBeine) {
				OrbCounter++;
			}
		}
		else if (OrbCounter == 3) {
			if (GotOrbKrallen) {
				OrbKopf = false;
				OrbBeine = false;
				OrbKrallen = true;
				OrbFlügel = false;
			}
			else if (!GotOrbKrallen) {
				OrbCounter++;
			}
		}
		else if (OrbCounter == 4) {
			if (GotOrbFlügel) {
				OrbKopf = false;
				OrbBeine = false;
				OrbKrallen = false;
				OrbFlügel = true;
			}
			else if (!GotOrbFlügel) {
				OrbCounter = 1;
			}
		}
		else if (OrbCounter == 5) {
			OrbCounter = 1;
		}

//---------------------------- Orb-Controller ------------------------------

	}
}
