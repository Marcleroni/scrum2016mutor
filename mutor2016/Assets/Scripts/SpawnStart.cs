using UnityEngine;
using System.Collections;

public class SpawnStart : MonoBehaviour {

	public Transform player;
	public Transform cam;
	public Transform cameraSpawn;
	public GameObject gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("GameManager");
		GameManager manager = gameManager.GetComponent<GameManager>();

		if (manager.fromLevel == -1) {
			player.position = this.transform.position;
			cam.position = cameraSpawn.position;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
