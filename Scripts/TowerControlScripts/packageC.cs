using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class packageC : MonoBehaviour {

	private bool drawGUI = false;
	    
	public GameObject towerC;
	public GameObject towerD;
	public GameObject packageD;

	public GameObject TextC;
	public GameObject TextI;

	void Start () {


		
	}
	

	void Update () {

		if (drawGUI == true && Input.GetKeyDown (KeyCode.F)) {
			
			Destroy (towerC);
			towerD.SetActive (true);
			packageD.SetActive (true);
			TextC.SetActive (true);
			destroyTextI ();
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

	void destroyTextI(){
		TextI.SetActive (false);
	}



		
}
