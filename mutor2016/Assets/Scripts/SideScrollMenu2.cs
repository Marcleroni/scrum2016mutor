using UnityEngine;
using System.Collections;

public class SideScrollMenu2 : MonoBehaviour {

	public float speed = 1f;
	public Vector3 resetEnd;
	public Vector3 resetStart;
	Transform tf;

	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform> ();
	}

	// Update is called once per frame
	void Update () {
		tf.Translate(Vector3.left*speed);
		if (tf.position.x < -13.68) {
			tf.position = new Vector3(15.11f,-1.54f,0f);
		}
	}
}
