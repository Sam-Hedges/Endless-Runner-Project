using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Project.Runtime._Scripts.MenuScripts
{
    public class UiControllerOptions : MonoBehaviour
    {
        private Button menu;

		// Start is called before the first frame update
		void Start()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
            
            menu = root.Q<Button>("Return-button");

            menu.clicked += ReturnMenuPressed;

        }

        void ReturnMenuPressed()
        {
			SceneManager.LoadScene("SampleScene");	
		}
    }
}
