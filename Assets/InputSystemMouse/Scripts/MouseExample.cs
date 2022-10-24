using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseExample : MonoBehaviour
{
    MouseActions actions;

    // Start is called before the first frame update
    void Start()
    {
        actions = new MouseActions();
        actions.Gameplay.Enable();

        actions.Gameplay.MousePosition.performed += OnMouseMoved;
        actions.Gameplay.LeftClick.performed += OnClick;
        actions.Gameplay.RightClick.performed += OnClick;
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        bool val = context.ReadValueAsButton();

        if (val)
            Debug.Log($"{context.action.name} Down");
        else
            Debug.Log($"{context.action.name} Up");
    }

    private void OnMouseMoved(InputAction.CallbackContext context)
    {
        //callback
        //Debug.Log(context.ReadValue<Vector2>());
    }

    private void OnGUI()
    {
        //polling
        GUILayout.Label(actions.Gameplay.MousePosition.ReadValue<Vector2>().ToString());
    }
}
