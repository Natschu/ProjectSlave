using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	public void ContinueGame()
	{
		GameManager.instance.ResumeGame();
	}

	public void RestartLevel()
	{
		GameManager.instance.RestartLevel();
	}

	//public void LoadSettings()
	//{
	//	//Set Settings to active etc -> no func needed?
	//}

	public void LoadMainMenu()
	{
		GameManager.instance.LoadMainMenu();
	}

	public void ExitGame()
	{
		GameManager.instance.ExitGame();
	}
}
