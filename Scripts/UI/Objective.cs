using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour {

	public GameObject mainCam;
	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Return)) {

			player.SetActive (true);
			mainCam.SetActive (false);
			Destroy (gameObject);
		}
	}
}
