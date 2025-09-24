using UnityEngine;
using UnityEngine.UIElements;

public class UITOOLKITBUTTONTEST : MonoBehaviour
{
    private UIDocument document;
    public Button button;

    private void Awake()
    {
        button = document.rootVisualElement.Q("Button") as Button;
        button.RegisterCallback<ClickEvent>(OnPlayGameClick);
    }

    private void OnPlayGameClick(ClickEvent evt)
    {
        Debug.Log("Start Game Button Pressed");
    }

    private void OnDisable()
    {
        button.UnregisterCallback<ClickEvent>(OnPlayGameClick);
    }
}
