using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _document;
    private Button _StartButton;
    private Button _CreditsButton;
    private Button _QuitButton;
    private VisualElement Container;

    public float fadeDuration = 2f;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        Container = _document.rootVisualElement.Q<VisualElement>("Container");

        _StartButton = _document.rootVisualElement.Q("StartGameButton") as Button;
        _CreditsButton = _document.rootVisualElement.Q("CreditsButton") as Button;
        _QuitButton = _document.rootVisualElement.Q("QuitButton") as Button;

        _StartButton.RegisterCallback<ClickEvent>(OnPlayGameClick);
        _CreditsButton.RegisterCallback<ClickEvent>(OnCreditsClick);
        _QuitButton.RegisterCallback<ClickEvent>(OnQuitClick);

    }

    private void OnPlayGameClick(ClickEvent evt)
    {
        Debug.Log("Start Game Button Pressed");
        FadeOutFunction();
    }

    private void OnCreditsClick(ClickEvent evt)
    {
        Debug.Log("Credits Button Pressed");
        
    }

    private void OnQuitClick(ClickEvent evt)
    {
        Debug.Log("Quit Button Pressed");
        Application.Quit();
    }

    private void OnDisable()
    {
        _StartButton.UnregisterCallback<ClickEvent>(OnPlayGameClick);
        _CreditsButton.UnregisterCallback<ClickEvent>(OnPlayGameClick);
        _QuitButton.UnregisterCallback<ClickEvent>(OnPlayGameClick);
    }

    public void FadeOutFunction()
    {
        StartCoroutine(FadeOutUI());
        StartCoroutine(WaitThenLoadGame());
    }

    IEnumerator FadeOutUI()
    {
        Container.style.display = DisplayStyle.Flex;

        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            float alpha = 1 - (elapsed / fadeDuration);
            Container.style.opacity = alpha;
            elapsed += Time.deltaTime;
            yield return null;
        }

        Container.style.opacity = 0f;
    }

    IEnumerator WaitThenLoadGame()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }

}
