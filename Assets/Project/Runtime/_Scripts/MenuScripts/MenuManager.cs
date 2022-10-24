using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

	[SerializeField] private GameObject playerUI;
	[SerializeField] private GameObject options;
	[SerializeField] private GameObject menu;
	[SerializeField] private GameObject deathScreen;
	[SerializeField] private GameObject pause;

	[SerializeField] private GameObject story;

	public void Awake()
	{
		Time.timeScale = 0;
	}

	#region this allows the player to be able to click to go to the next story
	public void NextScene()
	{
		story.SetActive(false);
	}
	#endregion

	#region Skips the story scene to got to main menu
	public void SkipScene()
	{		
		menu.SetActive(true);
		story.SetActive(false);
	}
	#endregion

	#region This is the menu set up settings
	public void PlayGame()
	{
		menu.SetActive(false);
		playerUI.SetActive(true);
		Time.timeScale = 1;
	}

	public void QuitGame()
	{
		Application.Quit();
		Debug.Log("Game will close when button is pressed");	
	}

	public void OptionsMenuu()
	{
		playerUI.SetActive(false);
		options.SetActive(true);
		Time.timeScale = 0;
	}

	public void ReturnToGame()
	{
		options.SetActive(false);
		playerUI.SetActive(true);
		Time.timeScale = 1;
	}

	public void Pause()
	{
		menu.SetActive(false);
		pause.SetActive(true);
		Time.timeScale = 0;
	}

	public void ReturnFromPause()
	{
		pause.SetActive(false);
		playerUI.SetActive(true);
		Time.timeScale = 1;
	}
	#endregion
}