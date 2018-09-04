using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class packageB : MonoBehaviour {

	private bool drawGUI = false;
	public GameObject towerB;
	public GameObject towerA;
	public GameObject packageA;
	public GameObject TextC;

	void Start () {
		Invoke ("destroyTextC", 4.5f);
	}


	void Update () {

		if (drawGUI == true && Input.GetKeyDown (KeyCode.F)) {

			Destroy (towerB);
			towerA.SetActive (true);
			packageA.SetActive (true);
			TextC.SetActive (true);
			Destroy (gameObject);
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

		TextC.SetActive (false);
	}
}