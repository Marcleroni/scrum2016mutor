using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UILeben : MonoBehaviour {

	Animator anim;
	public GameObject gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag("GameManager");
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		GameManager manager = gameManager.GetComponent<GameManager>();
		anim.SetInteger ("Leben", manager.Leben);
	}
}
