using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour {

	public void PlayAgainButton(string newLevel)
	{
		SceneManager.LoadScene (newLevel);
	}

	public void ExitButton ()
	{
		Application.Quit ();
	}

	public void ExitToMMBtn(string MainMenu)
	{
		SceneManager.LoadScene (MainMenu);
	}
}
