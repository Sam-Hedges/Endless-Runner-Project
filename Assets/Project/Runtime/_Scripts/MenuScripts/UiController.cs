using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Project.Runtime._Scripts.MenuScripts
{
    public class UiController : MonoBehaviour
    {
        //C67FE5

        private Button startButton;
        private Button quitGame;

		// Start is called before the first frame update
		private void Start()
        {
			var root = GetComponent<UIDocument>().rootVisualElement;

            startButton = root.Q<Button>("Start-button");
            quitGame = root.Q<Button>("Quit-button");

            startButton.clicked += StartButtonPressed;
            quitGame.clicked += QuitGameButtonPressed;
        }

        private void StartButtonPressed()
        {
			SceneManager.LoadScene("SampleScene");	
		}

        private void QuitGameButtonPressed()
        {
            Application.Quit();
            Debug.Log("Game would close");
        }
    }
}
