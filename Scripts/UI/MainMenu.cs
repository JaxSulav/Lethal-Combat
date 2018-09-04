using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void NewGameButton(string newLevel)
	{
		SceneManager.LoadScene (newLevel);
	}

	public void MultiplayerBtn(string multiPlayer)
	{
		SceneManager.LoadScene (multiPlayer);
	}

	public void ExitButton ()
	{
		Application.Quit ();
	}
}

