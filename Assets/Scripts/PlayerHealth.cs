using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    public Rigidbody2D rb;
    public UIDocument DeathUIDoc;
    public VisualElement Container;
    public DeathUIEvents gameManager;
    // public Button Respawn;

    public float maxHealth = 10f;
    public float currentHealth = 10f;
    public float damageScaler = 1.0f;

    private bool isSafe = false;

    public int CollectedLightbulbsNumber = 0;

    public bool hasDied = false;
    private float targetOpacity = 0f; 
    public float lerpSpeed = 2f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Container = DeathUIDoc.rootVisualElement.Q<VisualElement>("Container");
        // Respawn = DeathUIDoc.rootVisualElement.Q<Button>("Respawn");
    }

    void FixedUpdate()
    {
        if (currentHealth >= 0)
        {
            if (isSafe == false)
            {
                currentHealth -= Time.deltaTime * damageScaler;
            }
        }

        if (currentHealth <= 0 && !hasDied)
        {
            hasDied = true;
            targetOpacity = targetOpacity == 1f ? 0f : 1f;
            //Debug.Log("DeathStarted");
            gameManager.GameOver();
        }

        if (hasDied)
        {
            float currentOpacity = Container.resolvedStyle.opacity;
            float newOpacity = Mathf.Lerp(currentOpacity, targetOpacity, lerpSpeed * Time.deltaTime);
            Container.style.opacity = newOpacity;
        }   
    }
 
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SafeZone"))
        {
            //Debug.Log("safe!");
            isSafe = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("SafeZone"))
        {
            //Debug.Log("not safe!");
            isSafe = false;
        }
    }
}
