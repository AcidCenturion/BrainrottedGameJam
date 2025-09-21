using UnityEngine;
using UnityEngine.UIElements;

public class GameUIHandler : MonoBehaviour
{
    public PlayerHealth PlayerHealth;
    public UIDocument UIDoc;
    public VisualElement HealthBarFill;

    private void Start()
    {
        HealthBarFill = UIDoc.rootVisualElement.Q<VisualElement>("HealthBarFill");
    }

    void FixedUpdate()
    {
        HealthChanged();
    }

    void HealthChanged()
    {
        float healthRatio = (float)PlayerHealth.currentHealth / PlayerHealth.maxHealth;
        float healthWidth = Mathf.Lerp(0, 100, healthRatio);
        HealthBarFill.style.width = Length.Percent(healthWidth);
    }
    
}
