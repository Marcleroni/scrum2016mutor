using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public AudioClip level1;
	public AudioClip Boss1;
	public AudioSource audioS;

	// Use this for initialization
	void Start () {
		audioS = GetComponent<AudioSource>();
		audioS.clip = level1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
