using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

	public void ReloadLevel()
	{
		//GameManager.instance.Invoke("func", time);
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
