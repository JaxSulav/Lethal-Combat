using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doors : MonoBehaviour {

	Animator anim; 

	private bool drawGUI = false;

	void Start()
	{
		
		anim = GetComponent<Animator> ();
	}

	void Update (){
		if (drawGUI == true && Input.GetKeyDown(KeyCode.E)) { 

			anim.SetBool ("open", true);
			AudioSource audio = GetComponent<AudioSource> ();
			audio.Play ();

		} 
	}


	void  OnTriggerEnter ( Collider theCollider  ){ 
		if(theCollider.tag == "Player")  
		{
			
			drawGUI= true;
		}     
	}

	void  OnTriggerExit ( Collider theCollider  ){ 
		if (theCollider.tag == "Player") { 
			drawGUI = false;
			anim.SetBool ("open", false);
		} 

	}


	void  OnGUI (){
		if(drawGUI == true)
		{
			GUI.Box( new Rect(Screen.width*0.5f-51, 200, 110, 23), "Press E To Open");
		}
	}
		
}