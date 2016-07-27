using UnityEngine;
using System.Collections;

public class SpawnManagerFrom1 : MonoBehaviour {

	public Transform Player;
	public GameObject gameManager;
	public Transform MainCam;
	public Transform camSpawn;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag("GameManager");
		GameManager manager = gameManager.GetComponent<GameManager>();
		if (manager.fromLevel == 0) {
			Player.position = this.transform.position;
			MainCam.position = camSpawn.position;
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
