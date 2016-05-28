using UnityEngine;
using System.Collections;

public class SpriteChanger : MonoBehaviour {

	public GameObject gameManager;

	SpriteRenderer rend;

	public bool idleBool = false;
	public Sprite idleNormal;
	public Sprite idleKopf;


	// Use this for initialization
	void Start () {
		rend = GetComponent<SpriteRenderer>();
		gameManager = GameObject.FindGameObjectWithTag("GameManager");
	}
	
	// Update is called once per frame
	void LateUpdate () {
		GameManager manager = gameManager.GetComponent<GameManager>();
		//if (idleBool) {
			if (manager.OrbKopf == true) {
				rend.sprite = idleKopf;
				Debug.Log ("Kopf");
				idleBool = false;
			} else if (manager.OrbKopf == false)  {
				rend.sprite = idleKopf;
				Debug.Log("NichtKopf");
				idleBool = false;
			}
		//}

	}

	public void idle () {
		idleBool = true;
	}
}
