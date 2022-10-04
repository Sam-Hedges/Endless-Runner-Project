using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Project.Runtime._Scripts.MenuScripts
{
    public class UiControllerOptions : MonoBehaviour
    {
        private Button _returnToMenu;
        
        // Start is called before the first frame update
        void Start()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
            
            _returnToMenu = root.Q<Button>("Return-button");

            _returnToMenu.clicked += ReturnMenuPressed;
        }

        void ReturnMenuPressed()
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
