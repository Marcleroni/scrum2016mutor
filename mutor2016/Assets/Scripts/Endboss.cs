using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Endboss : MonoBehaviour {

	public GameObject laserBeam;
	public Transform spawn;
	Animator anim;

	public GameObject canvas;
	public GameObject credits;
	public GameObject gameManager;


	public int BossLeben = 5;
	public bool alive = true;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		gameManager = GameObject.FindGameObjectWithTag("GameManager");
	}
	
	// Update is called once per frame
	void Update () {

		if (BossLeben < 1) {
			alive = false;
			anim.SetBool ("Death", true);
		}
	}

	void Shoot () {

		if (alive) {
			GameObject beam = (GameObject)Instantiate (laserBeam, spawn.position, spawn.rotation);
		}
	}

	public void End () {
		canvas.SetActive (false);
		credits.SetActive (true);
	}

	public void Return () {
		GameManager manager = gameManager.GetComponent<GameManager>();
		manager.lebenTotal = 3;
		manager.SplitterCounter = 0;
		manager.Items.Clear();
		manager.Bosse.Clear();
		manager.OrbCounter = 0;
		manager.OrbKopf = false;
		manager.GotOrbKopf = false;

		manager.OrbBeine = false;
		manager.GotOrbBeine = false;

		manager.OrbKrallen = false;
		manager.GotOrbKrallen = false;

		manager.OrbFlügel = false;
		manager.GotOrbFlügel = false;
		SceneManager.LoadScene ("StartMenu");
	}
}
