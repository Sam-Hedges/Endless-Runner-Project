using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UserController : MonoBehaviour
{
	private Button pauseButton;
	private Button optionsButton;
	private Button menuButton;

	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private GameObject userMenu;

    // Start is called before the first frame update
    private void Start()
    {
		var root = GetComponent<UIDocument>().rootVisualElement;

		pauseButton = root.Q<Button>("Pause-Button");
		optionsButton = root.Q<Button>("Options-Button");
		menuButton = root.Q<Button>("Menu-Button");

		pauseButton.clicked += PauseMenu;
		optionsButton.clicked += OptionsMenu;
		menuButton.clicked += MenuButton;
	}

    private void PauseMenu()
	{
		pauseMenu.SetActive(true);
		userMenu.SetActive(false);
	}

	private void OptionsMenu()
	{
		SceneManager.LoadScene("Options");
	}

	private void MenuButton()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
