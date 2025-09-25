using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class WinConditionEvents : MonoBehaviour
{
    private UIDocument WinConditionDocument;
    private Button DoBetterButton;
    private Button MainMenuButton;

    public PlayerCollect PlayerCollect;
    public GameUIHandler GameUIHandler;
    public GameObject WinUIManager;

    void Start()
    {
        WinConditionDocument = GetComponent<UIDocument>();
        DoBetterButton = WinConditionDocument.rootVisualElement.Q("DoBetterButton") as Button;
        MainMenuButton = WinConditionDocument.rootVisualElement.Q("MainMenuButton") as Button;
        VisualElement root = WinConditionDocument.rootVisualElement;
        Label TimeLabel = root.Q<Label>("Time");

        DoBetterButton.RegisterCallback<ClickEvent>(OnDoBetterButtonClick);
        MainMenuButton.RegisterCallback<ClickEvent>(OnMainMenuButtonClick);
        
        if (TimeLabel != null)
        {
            float temp2 = (int)GameUIHandler.playedTime;
            TimeLabel.text = "Time Completed: " + (60-temp2).ToString() + " seconds";
        }
    }

    private void OnDoBetterButtonClick(ClickEvent evt)
    {
        Debug.Log("Respawn Button Pressed");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnMainMenuButtonClick(ClickEvent evt)
    {
        Debug.Log("MainMenu Button Pressed");
        SceneManager.LoadScene(0);
    }

    private void OnDisables()
    {
        DoBetterButton.UnregisterCallback<ClickEvent>(OnDoBetterButtonClick);
        MainMenuButton.UnregisterCallback<ClickEvent>(OnDoBetterButtonClick);
    }
}
