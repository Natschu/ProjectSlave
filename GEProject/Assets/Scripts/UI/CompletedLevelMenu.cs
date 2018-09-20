using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletedLevelMenu : MonoBehaviour {

	public void CompletedLevel()
	{
		GameManager.instance.GoToNextLevel();
	}

	public void RestartLevel()
	{
		GameManager.instance.RestartLevel();
	}

	public void LoadMainMenu()
	{
		GameManager.instance.LoadMainMenu();
	}

	public void ExitGame()
	{
		GameManager.instance.ExitGame();
	}
}
