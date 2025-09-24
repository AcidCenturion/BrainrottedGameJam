using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    public Rigidbody2D rb;
    public UIDocument DeathUIDoc;
    public GameObject DeathUIManager;
    public GameOverEvents GameOverEvents;

    public float maxHealth = 10f;
    public float currentHealth = 10f;
    public float damageScaler = 1.0f;

    private bool isSafe = false;

    public int CollectedLightbulbsNumber = 0;

    public bool hasDied = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            //Debug.Log("DeathStarted");
            DeathUIManager.SetActive(true);
        }

        if (hasDied)
        {
            GameOverEvents.FadeInFunction();
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
