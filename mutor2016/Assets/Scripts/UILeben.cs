using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UILeben : MonoBehaviour {

	public GameObject gameManager;
	Text LebenUI;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag("GameManager");
		LebenUI = gameObject.GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		GameManager manager = gameManager.GetComponent<GameManager>();
		LebenUI.text = ("Leben: " + manager.Leben.ToString());
	}
}
