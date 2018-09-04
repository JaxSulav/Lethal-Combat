using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour {

	public GameObject mainCam;
	public GameObject player;
	public GameObject pauseCanvas;

	public void ResumeBtn ()
	{
		pauseCanvas.SetActive (false);
		mainCam.SetActive (false);
		player.SetActive (true);
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = false;
		EnemyAI.isPlayerAlive = true;
	}

	public void MainMenuBtn(string mainMenu)
	{
		SceneManager.LoadScene (mainMenu);
	}

	public void RestartBtn (string lvl1)
	{
		SceneManager.LoadScene (lvl1);
	}

	public void ExitBtn()
	{
		Application.Quit ();
	}
}
