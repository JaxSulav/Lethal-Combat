using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public float enemyHealth;
	Animator anim;
	void Start () {
		anim = GetComponent<Animator> ();
	}
	

	void Update () {
		if (enemyHealth < 0f) {
		
			anim.SetTrigger ("isDead");
			Invoke ("Destroy", 4f);

		
		}
	}

	void Destroy(){

		Destroy (gameObject);
	}
}
