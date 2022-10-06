using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
	private Button returnToMenu;
	private Button returnToGame;

	[SerializeField] private GameObject gamePlay;
	[SerializeField] private GameObject pauseMenu;

    // Start is called before the first frame update
    private void Start()
    {
		var root = GetComponent<UIDocument>().rootVisualElement;

		returnToGame = root.Q<Button>("Return-Game");
		returnToMenu = root.Q<Button>("Return-Menu");

		returnToGame.clicked += ReturnGame;
		returnToMenu.clicked += ReturnMenu;
	}

    private void ReturnGame()
	{

		SceneManager.LoadScene("SampleScene");
		gamePlay.SetActive(true);
	}

	private void ReturnMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
