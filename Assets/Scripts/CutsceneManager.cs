using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class CutsceneManager : MonoBehaviour
{
    private UIDocument _document;
    private VisualElement Blackout1;
    private VisualElement Blackout2;
    private VisualElement Blackout3;

    private float fadeDuration = 1f;

    private int comicPanelNumber = 0;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        Blackout1 = _document.rootVisualElement.Q<VisualElement>("Blackout1");
        Blackout2 = _document.rootVisualElement.Q<VisualElement>("Blackout2");
        Blackout3 = _document.rootVisualElement.Q<VisualElement>("Blackout3");

        Blackout1.style.opacity = 1f;
        Blackout2.style.opacity = 1f;
        Blackout3.style.opacity = 1f;
        Blackout1.style.display = DisplayStyle.Flex;
        Blackout2.style.display = DisplayStyle.Flex;
        Blackout3.style.display = DisplayStyle.Flex;
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
                FadeOutB3Function();
                Debug.Log(comicPanelNumber);
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

    private void FadeOutB3Function()
    {
        StartCoroutine(FadeOutB3());
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

    IEnumerator FadeOutB3()
    {
        Blackout3.style.display = DisplayStyle.Flex;
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            float alpha = 1 - (elapsed / fadeDuration);
            Blackout3.style.opacity = alpha;
            elapsed += Time.deltaTime;
            yield return null;
        }
        Blackout3.style.opacity = 0f;
        Blackout3.style.display = DisplayStyle.None;
    }

}
