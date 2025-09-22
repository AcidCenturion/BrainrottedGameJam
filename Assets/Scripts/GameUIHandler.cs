using UnityEngine;
using UnityEngine.UIElements;

public class GameUIHandler : MonoBehaviour
{
    public PlayerHealth PlayerHealth;
    public PlayerCollect PlayerCollect;
    public Lightbulb lightbulb;
    public UIDocument UIDoc;

    public VisualElement HealthBarFill;
    public VisualElement LitLightbulb1;
    public VisualElement LitLightbulb2;
    public VisualElement LitLightbulb3;
    public VisualElement LitLightbulb4;
    public VisualElement LitLightbulb5;
    private Label Timer;

    public float StartTime = 500f;

    private void Start()
    {
        HealthBarFill = UIDoc.rootVisualElement.Q<VisualElement>("HealthBarFill");

        LitLightbulb1 = UIDoc.rootVisualElement.Q<VisualElement>("LitLightbulb1");
        LitLightbulb2 = UIDoc.rootVisualElement.Q<VisualElement>("LitLightbulb2");
        LitLightbulb3 = UIDoc.rootVisualElement.Q<VisualElement>("LitLightbulb3");
        LitLightbulb4 = UIDoc.rootVisualElement.Q<VisualElement>("LitLightbulb4");
        LitLightbulb5 = UIDoc.rootVisualElement.Q<VisualElement>("LitLightbulb5");

        Timer = UIDoc.rootVisualElement.Q<Label>("Timer");
    }

    void FixedUpdate()
    {
        HealthChanged();
        LightbulbCollects();
        TimerCountdown();
    }

    void HealthChanged()
    {
        float healthRatio = (float)PlayerHealth.currentHealth / PlayerHealth.maxHealth;
        float healthWidth = Mathf.Lerp(0, 100, healthRatio);
        HealthBarFill.style.width = Length.Percent(healthWidth);
    }

    void LightbulbCollects()
    {
        if (PlayerCollect.PlayerCollectNumber == 1)
        {
            LitLightbulb1.style.opacity = 1.0f;
        }
        if (PlayerCollect.PlayerCollectNumber == 2)
        {
            LitLightbulb2.style.opacity = 1.0f;
        }
        if (PlayerCollect.PlayerCollectNumber == 3)
        {
            LitLightbulb3.style.opacity = 1.0f;
        }
        if (PlayerCollect.PlayerCollectNumber == 4)
        {
            LitLightbulb4.style.opacity = 1.0f;
        }
        if (PlayerCollect.PlayerCollectNumber == 5)
        {
            LitLightbulb5.style.opacity = 1.0f;
        }
    }

    void TimerCountdown()
    {
        float temp = StartTime -= Time.deltaTime;
        Timer.text = ((int)temp).ToString();
    }
}
