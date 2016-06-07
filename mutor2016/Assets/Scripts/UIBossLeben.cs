using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIBossLeben : MonoBehaviour {

	public GameObject boss;
	Text LebenUI;

	// Use this for initialization
	void Start () {
		LebenUI = gameObject.GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		Boss1 bossscript = boss.GetComponent<Boss1>();
		LebenUI.text = ("Boss Leben: " + bossscript.BossLeben.ToString());
	}
}
