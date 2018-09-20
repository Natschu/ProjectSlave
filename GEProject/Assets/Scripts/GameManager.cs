using UnityEngine;
using UnityEngine.SceneManagement;

using System.Collections;

public class GameManager : MonoBehaviour {

	public static bool gameIsPaused = false;
	public static string mainMenuName = "MainMenu";
	public static string creditsMenuName = "Credits";

	public float transitionTime = 2f;

	public GameObject crosshair;
	public GameObject menuBackground;
	public GameObject gameOverUI;
	public GameObject pauseMenuUI;
	public GameObject completedLevelUI;

	private bool levelEnded = false;
	private bool disablePause = false;

	private bool disableCrossHair = false;
	private bool disableMenuBackground = false;
	private bool disableGameOverUI = false;
	private bool disablePauseMenuUI = false;
	private bool disableCompletedLevelUI = false;

	private IEnumerator coroutine;

	#region Singleton
	public static GameManager instance;
	private void Singleton()
	{
		if(instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
		//DontDestroyOnLoad(gameObject);
	}
	#endregion

	void Awake()
	{
		Singleton();

		gameIsPaused = false;
		Cursor.visible = false;
		if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName(mainMenuName) ||
		   SceneManager.GetActiveScene() == SceneManager.GetSceneByName(creditsMenuName))
		{
			Cursor.visible = true;
		}
		Time.timeScale = 1f;
	}


	void Start()
	{
		#region Check References
		if (crosshair == null)
		{
			disableCrossHair = true;
		}

		if (menuBackground == null)
		{
			disableMenuBackground = true;
		}

		if (gameOverUI == null)
		{
			disableGameOverUI = true;
		}

		if (pauseMenuUI == null)
		{
			disablePauseMenuUI = true;
		}

		if (completedLevelUI == null)
		{
			disableCompletedLevelUI = true;
		}
		#endregion
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && !levelEnded)
		{
			if (!gameIsPaused)
			{
				PauseGame();
			}
			else
			{
				ResumeGame();
			}
		}
	}

	//Call UI Functions
	public void CompleteLevel()
	{
		Debug.Log("Completed Level");

		if(levelEnded == false)
		{
			levelEnded = true;
			gameIsPaused = true;

			DisableCrosshair();
			EnableBackground();
			EnableCompletedLevelUI();

			Cursor.visible = true;
			StopTime();
		}
	}
	public void GameOver()
	{
		if (levelEnded == false)
		{
			Debug.Log("Game Over");

			levelEnded = true;
			gameIsPaused = true;

			DisableCrosshair();
			EnableBackground();
			EnableGameOverUI();

			Cursor.visible = true;
			StopTime();
		}
	}
	public void PauseGame()
	{
		if (!disablePause)
		{
			Debug.Log("Pause Game");

			DisableCrosshair();
			EnableBackground();
			EnablePauseMenuUI();

			gameIsPaused = true;
			Cursor.visible = true;
			StopTime();
		}
	}
	public void ResumeGame()
	{
		if (!disablePause)
		{
			Debug.Log("Resume Game");

			ResumeTime();

			DisabledPauseMenuUI();
			DisableBackground();
			EnableCrosshair();

			Cursor.visible = false;
			gameIsPaused = false;
		}
	}

	//Scene Functions
	public void GoToNextLevel()
	{
		Debug.Log("Reloading Level");

		levelEnded = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
	public void RestartLevel()
	{
		Debug.Log("Reloading Level");

		levelEnded = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	public void LoadMainMenu()
	{
		Debug.Log("Loading Main Menu");

		levelEnded = false;
		SceneManager.LoadScene(mainMenuName);
	}
	public void ExitGame()
	{
		Debug.Log("Exit Game");

		Application.Quit();
	}

	//Enable/Disable Functions
	private void EnableCrosshair()
	{
		if (!disableCrossHair)
		{
			crosshair.SetActive(true);
		}
	}
	private void DisableCrosshair()
	{
		if (!disableCrossHair)
		{
			crosshair.SetActive(false);
		}
	}

	private void EnableBackground()
	{
		if (!disableMenuBackground)
		{
			menuBackground.SetActive(true);
		}
	}
	private void DisableBackground()
	{
		if (!disableMenuBackground)
		{
			menuBackground.SetActive(false);
		}
	}

	private void EnableGameOverUI()
	{
		if (!disableGameOverUI)
		{
			gameOverUI.SetActive(true);
		}
	}
	private void DisabledGameOverUI()
	{
		if (!disableGameOverUI)
		{
			gameOverUI.SetActive(false);
		}
	}

	private void EnablePauseMenuUI()
	{
		if (!disablePauseMenuUI)
		{
			pauseMenuUI.SetActive(true);
		}
	}
	private void DisabledPauseMenuUI()
	{
		if (!disablePauseMenuUI)
		{
			pauseMenuUI.SetActive(false);
		}
	}

	private void EnableCompletedLevelUI()
	{
		if (!disableCompletedLevelUI)
		{
			completedLevelUI.SetActive(true);
		}
	}
	private void DisabledCompletedLevelUI()
	{
		if (!disableCompletedLevelUI)
		{
			completedLevelUI.SetActive(false);
		}
	}


	//Time Functions
	private void StopTime()
	{
		//StopCoroutine(coroutine);
		coroutine = ChangeTimeSpeed(0f);
		StartCoroutine(coroutine);	
	}
	private void ResumeTime()
	{
		StopCoroutine(coroutine);
		Time.timeScale = 1f;
	}
	
	private IEnumerator ChangeTimeSpeed(float endTimeScale)
	{
		disablePause = true;

		float timeBegin = Time.unscaledTime;
		float timeEnd = transitionTime + Time.unscaledTime;
		float currentTimeScale = Time.timeScale;

		Debug.Log("[Time] " + timeBegin + " => " + timeEnd);
		Debug.Log("[TimeScale] " + currentTimeScale + " => " + endTimeScale);
		
		//unscaledTime

		while(Time.unscaledTime <= timeEnd)
		{
			float lerpTime = UtilityFunctions.Remap(Time.unscaledTime,
							 timeBegin, timeEnd, 0f, 1f);

			Time.timeScale = Mathf.Lerp(currentTimeScale, endTimeScale,
										lerpTime);
			//Debug.Log(Time.timeScale);
			yield return null;
		}

		disablePause = false;
		Time.timeScale = endTimeScale;
	}
}
