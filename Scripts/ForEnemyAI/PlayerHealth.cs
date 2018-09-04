using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
	public float currentHealth;
	public float maxHealth;
	public GameObject mainCam;
	public GameObject deadCanvas;
	public GameObject pauseCanvas;
	public GameObject TextI;
	public GameObject radicule;
	public GameObject HbarCanvas;



	// Use this for initialization
	void Start () {

		HbarCanvas.SetActive (true);
		radicule.SetActive (true);
		TextI.SetActive (true);

	}

	// Update is called once per frame
	void Update () {
		
		if (EnemyAI.isPlayerAlive == false) {
			HbarCanvas.SetActive (false);
		}
		//for pause:::

		if (Input.GetKeyDown(KeyCode.Escape))	
			{
			
			mainCam.SetActive (true);
			gameObject.SetActive (false);
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		    pauseCanvas.SetActive (true);
			EnemyAI.isPlayerAlive = false;

			}

		if (currentHealth <= 0) {
			deadCanvas.SetActive (true);
			mainCam.SetActive (true);
			Destroy (gameObject);
		}
	}

	public void OnDestroy ()
	{
		EnemyAI.isPlayerAlive = false;

		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;


	}



}
