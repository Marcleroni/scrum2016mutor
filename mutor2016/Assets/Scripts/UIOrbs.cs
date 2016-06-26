using UnityEngine;
using System.Collections;

public class UIOrbs : MonoBehaviour {

	public GameObject gameManager;
	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		gameManager = GameObject.FindGameObjectWithTag("GameManager");	
	}
	
	// Update is called once per frame
	void Update () {
		GameManager manager = gameManager.GetComponent<GameManager>();

		anim.SetBool ("GotBeine", manager.GotOrbBeine);
		anim.SetBool ("GotKrallen", manager.GotOrbKrallen);
		anim.SetBool ("GotKopf", manager.GotOrbKopf);
		anim.SetBool ("GotFlügel", manager.GotOrbFlügel);

		anim.SetBool ("BeineAktiv", manager.OrbBeine);
		anim.SetBool ("KrallenAktiv", manager.OrbKrallen);
		anim.SetBool ("KopfAktiv", manager.OrbKopf);
		anim.SetBool ("FlügelAktiv", manager.OrbFlügel);
	}
}
