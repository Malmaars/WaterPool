using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem; // only if you use the new Input System

public class DebugUIToggle : MonoBehaviour
{
    [SerializeField] UIDocument uiDocument;
    [SerializeField] bool startVisible = false;

    void Start() => SetVisible(startVisible);

    void Update()
    {
        // Old Input Manager:
        if (Input.GetKeyDown(KeyCode.F1))
            SetVisible(uiDocument.rootVisualElement.style.display == DisplayStyle.None);

        // New Input System alternative:
        // if (Keyboard.current.f1Key.wasPressedThisFrame) ...
    }

    void SetVisible(bool visible)
    {
        uiDocument.rootVisualElement.style.display =
            visible ? DisplayStyle.Flex : DisplayStyle.None;
    }
}