using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverEvents : MonoBehaviour
{
    private UIDocument _document;
    private VisualElement Container;
    private Button _RespawnButton;
    private Button _MainMenuButton;

    public float fadeDuration = 1f;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        Container = _document.rootVisualElement.Q<VisualElement>("Container");

        _RespawnButton = _document.rootVisualElement.Q("RespawnButton") as Button;
        _MainMenuButton = _document.rootVisualElement.Q("MainMenuButton") as Button;
        _RespawnButton.RegisterCallback<ClickEvent>(OnRespawnClick);
        _MainMenuButton.RegisterCallback<ClickEvent>(OnMainMenuClick);

        Container.style.opacity = 0f;
        Container.style.display = DisplayStyle.None;
    }

    void Update()
    {
        FadeInFunction();
    }

    private void OnRespawnClick(ClickEvent evt)
    {
        Debug.Log("Respawn Button Pressed");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnMainMenuClick(ClickEvent evt)
    {
        Debug.Log("MainMenu Button Pressed");
        SceneManager.LoadScene(2);
    }

    private void OnDisable()
    {
        _RespawnButton.UnregisterCallback<ClickEvent>(OnRespawnClick);
        _RespawnButton.UnregisterCallback<ClickEvent>(OnMainMenuClick);
    }

    public void FadeInFunction()
    {
        StartCoroutine(FadeInUI());
    }

    IEnumerator FadeInUI()
    {
        Container.style.display = DisplayStyle.Flex;

        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            float alpha = elapsed / fadeDuration;
            Container.style.opacity = alpha;
            elapsed += Time.deltaTime;
            yield return null;
        }

        Container.style.opacity = 1f;
    }
}
