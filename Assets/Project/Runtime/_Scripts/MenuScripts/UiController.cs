using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Project.Runtime._Scripts.MenuScripts
{
    public class UiController : MonoBehaviour
    {
        //C67FE5

        public Button startButton;
        public Button optionsButton;
        public Button quitGame;
        
        // Start is called before the first frame update
        void Start()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            startButton = root.Q<Button>("Start-button");
            optionsButton = root.Q<Button>("Options-button");
            quitGame = root.Q<Button>("Quit-button");

            startButton.clicked += StartButtonPressed;
            quitGame.clicked += QuitGameButtonPressed;
            optionsButton.clicked += OptionsButtonPressed;
        }

        void StartButtonPressed()
        {
            SceneManager.LoadScene("SampleScene");
        }

        void OptionsButtonPressed()
        {
            SceneManager.LoadScene("Options");
        }

        void QuitGameButtonPressed()
        {
            Application.Quit();
            Debug.Log("Game would close");
        }
    }
}
