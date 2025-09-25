using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    private UIDocument _document;
    private VisualElement Blackout1;
    private VisualElement Blackout2;
    private VisualElement Blackout3;
    private VisualElement Blackout4;
    private VisualElement Blackout5;

    private float fadeDuration = 1f;

    private int comicPanelNumber = 0;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        Blackout1 = _document.rootVisualElement.Q<VisualElement>("Blackout1");
        Blackout2 = _document.rootVisualElement.Q<VisualElement>("Blackout2");
        Blackout3 = _document.rootVisualElement.Q<VisualElement>("Blackout3");
        Blackout4 = _document.rootVisualElement.Q<VisualElement>("Blackout4");
        Blackout5 = _document.rootVisualElement.Q<VisualElement>("Blackout5");

        Blackout1.style.opacity = 1f;
        Blackout2.style.opacity = 1f;
        Blackout3.style.opacity = 0f;
        Blackout4.style.opacity = 0f;
        Blackout5.style.opacity = 0f;
        Blackout1.style.display = DisplayStyle.Flex;
        Blackout2.style.display = DisplayStyle.Flex;
        Blackout3.style.display = DisplayStyle.None;
        Blackout4.style.display = DisplayStyle.None;
        Blackout5.style.display = DisplayStyle.None;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (comicPanelNumber == 0)
            {
                FadeOutB1Function();
                Debug.Log(comicPanelNumber);
            }

            else if (comicPanelNumber == 1)
            {
                FadeOutB2Function();
                Debug.Log(comicPanelNumber);
            }

            else if (comicPanelNumber == 2)
            {
                FadeInB3Function();
                Debug.Log(comicPanelNumber);
            }

            else if (comicPanelNumber == 3)
            {
                FadeInB4Function();
                Debug.Log(comicPanelNumber);
            }

            else if (comicPanelNumber == 4)
            {
                FadeInB5Function();
                Debug.Log(comicPanelNumber);
            }

            else if (comicPanelNumber == 5)
            {
                SceneManager.LoadScene(1);
            }

            comicPanelNumber += 1;
        }
        
    }

    private void FadeOutB1Function()
    {
        StartCoroutine(FadeOutB1());
    }

    private void FadeOutB2Function()
    {
        StartCoroutine(FadeOutB2());
    }

    private void FadeInB3Function()
    {
        StartCoroutine(FadeInB3());
    }

    private void FadeInB4Function()
    {
        StartCoroutine(FadeInB4());
    }

    private void FadeInB5Function()
    {
        StartCoroutine(FadeInB5());
    }

    IEnumerator FadeOutB1()
    {
        Blackout1.style.display = DisplayStyle.Flex;
        float elapsed1 = 0f;
        while (elapsed1 < fadeDuration)
        {
            float alpha1 = 1 - (elapsed1 / fadeDuration);
            Blackout1.style.opacity = alpha1;
            elapsed1 += Time.deltaTime;
            yield return null;
        }
        Blackout1.style.opacity = 0f;
        Blackout1.style.display = DisplayStyle.None;
    }

    IEnumerator FadeOutB2()
    {
        Blackout2.style.display = DisplayStyle.Flex;
        float elapsed2 = 0f;
        while (elapsed2 < fadeDuration)
        {
            float alpha2 = 1 - (elapsed2 / fadeDuration);
            Blackout2.style.opacity = alpha2;
            elapsed2 += Time.deltaTime;
            yield return null;
        }
        Blackout2.style.opacity = 0f;
        Blackout2.style.display = DisplayStyle.None;
    }

    IEnumerator FadeInB3()
    {
        Blackout3.style.display = DisplayStyle.Flex;
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            float alpha = elapsed / fadeDuration;
            Blackout3.style.opacity = alpha;
            elapsed += Time.deltaTime;
            yield return null;
        }
        Blackout3.style.opacity = 1f;
    }

    IEnumerator FadeInB4()
    {
        Blackout4.style.display = DisplayStyle.Flex;
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            float alpha = elapsed / fadeDuration;
            Blackout4.style.opacity = alpha;
            elapsed += Time.deltaTime;
            yield return null;
        }
        Blackout4.style.opacity = 1f;
    }

    IEnumerator FadeInB5()
    {
        Blackout5.style.display = DisplayStyle.Flex;
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            float alpha = elapsed / fadeDuration;
            Blackout5.style.opacity = alpha;
            elapsed += Time.deltaTime;
            yield return null;
        }
        Blackout5.style.opacity = 1f;
    }

}
