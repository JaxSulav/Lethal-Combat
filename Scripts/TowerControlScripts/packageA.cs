using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class packageA : MonoBehaviour {

	private bool drawGUI = false;
	public GameObject PackageA;
	public GameObject towerA;
	public GameObject TextC;
	public GameObject TextA;
	public GameObject InvokeHiddenDoorScript;

	void Start () {
		Invoke ("destroyTextC", 4.5f);
	}


	void Update () {

		if (drawGUI == true && Input.GetKeyDown (KeyCode.F)) {

			TextA.SetActive (true);
			InvokeHiddenDoorScript.SetActive (true);
			PackageA.SetActive (false);
		}
	}

	void OnTriggerEnter (Collider colC){
		if (colC.tag == "Player") {

			drawGUI = true;
		} 
	}

	void OnTriggerExit (Collider colC){
		if(colC.tag == "Player")  
		{

			drawGUI = false;
		} 
	}


	void OnGUI ()
	{
		if (drawGUI == true) {

			GUI.Box( new Rect(Screen.width*0.5f-51, 100, 130, 23), "Press F To Retrive");		
		}
	}

	void destroyTextC(){

		Destroy (TextC);
	}
}
