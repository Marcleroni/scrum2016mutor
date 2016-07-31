using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartButtonNewBehaviourScript : MonoBehaviour {

	public GameObject canv;
	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void startBt () {
		anim.SetBool ("start", true);
		canv.SetActive (false);
	}

	public void Exit () {
		Application.Quit ();
	}

	public void Switch () {
		SceneManager.LoadScene ("Level1");
	}
}
