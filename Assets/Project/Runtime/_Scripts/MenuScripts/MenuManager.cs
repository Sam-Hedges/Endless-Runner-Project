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
	[SerializeField] private GameObject story2;
	[SerializeField] private GameObject story3;
	[SerializeField] private GameObject story4;
	[SerializeField] private GameObject story5;
	[SerializeField] private GameObject story6;
	[SerializeField] private GameObject story7;
	[SerializeField] private GameObject story8;
	[SerializeField] private GameObject story9;
	[SerializeField] private GameObject story10;

	#region this allows the player to be able to click to go to the next story
	public void NextScene()
	{
		story.SetActive(false);
		story2.SetActive(true);
	}

	public void NextScene1()
	{
		story2.SetActive(false);
		story3.SetActive(true);
	}

	public void NextScene2()
	{
		story3.SetActive(false);
		story4.SetActive(true);
	}

	public void NextScene3()
	{
		story4.SetActive(false);
		story5.SetActive(true);
	}

	public void NextScene4()
	{
		story5.SetActive(false);
		story6.SetActive(true);
	}

	public void NextScene5()
	{
		story6.SetActive(false);
		story7.SetActive(true);
	}

	public void NextScene6()
	{
		story7.SetActive(false);
		story8.SetActive(true);
	}

	public void NextScene7()
	{
		story8.SetActive(false);
		story9.SetActive(true);
	}
	public void NextScene8()
	{
		story9.SetActive(false);
		story10.SetActive(true);
	}

	public void GameButtonMenu10()
	{
		story10.SetActive(false);
		menu.SetActive(true);
	}
	#endregion

	#region Skips the story scene to got to main menu
	public void SkipScene()
	{		
		menu.SetActive(true);
		story.SetActive(false);
	}

	public void SkipScene2()
	{
		menu.SetActive(true);
		story2.SetActive(false);
	}

	public void SkipScene3()
	{
		menu.SetActive(true);
		story3.SetActive(false);
	}

	public void SkipScene4()
	{
		menu.SetActive(true);
		story4.SetActive(false);
	}

	public void SkipScene5()
	{
		menu.SetActive(true);
		story5.SetActive(false);
	}

	public void SkipScene6()
	{
		menu.SetActive(true);
		story6.SetActive(false);
	}

	public void SkipScene7()
	{
		menu.SetActive(true);
		story7.SetActive(false);
	}

	public void SkipScene8()
	{
		menu.SetActive(true);
		story8.SetActive(false);
	}

	public void SkipScene9()
	{
		menu.SetActive(true);
		story9.SetActive(false);
	}
	#endregion

	#region This is the menu set up settings
	public void PlayGame()
	{
		menu.SetActive(false);
		playerUI.SetActive(true);
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
	}

	public void ReturnToGame()
	{
		options.SetActive(false);
		playerUI.SetActive(true);
	}

	public void Pause()
	{
		menu.SetActive(false);
		pause.SetActive(true);
	}

	public void ReturnFromPause()
	{
		pause.SetActive(false);
		playerUI.SetActive(true);
	}
	#endregion
}