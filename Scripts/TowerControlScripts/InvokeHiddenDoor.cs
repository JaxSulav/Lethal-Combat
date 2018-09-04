using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeHiddenDoor : MonoBehaviour {

	public GameObject TextA;
	void Start () {
		Invoke("destroyTextA", 4.5f);
	}
	
	void destroyTextA(){
		Destroy (TextA);
	}
}
